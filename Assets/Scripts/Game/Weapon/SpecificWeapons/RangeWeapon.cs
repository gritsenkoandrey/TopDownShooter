using System;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public abstract class RangeWeapon : BaseWeapon, IWeapon
    {
        private protected IWeaponFactory WeaponFactory;

        private int _clipCount;
        private float _attackDistance;
        private bool _canAttack;

        private Tween _speedAttackTween;
        private Tween _rechargeTimeTween;
        
        protected RangeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            SetCanAttack();
            SetAttackDistance();
            ReloadClip();
        }

        void IWeapon.Attack(ITarget target) => Shoot();
        bool IWeapon.CanAttack() => _clipCount > 0 && _canAttack;
        bool IWeapon.IsDetectThroughObstacle() => WeaponCharacteristic.IsDetectThroughObstacle;
        float IWeapon.AttackDistance() => _attackDistance;
        float IWeapon.DetectionDistance() => WeaponCharacteristic.DetectionDistance;

        private protected virtual void ReloadClip() => _clipCount = WeaponCharacteristic.ClipCount;
        private protected virtual void ReduceClip() => _clipCount--;
        private protected virtual int SetDamage() => WeaponCharacteristic.Damage;

        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(WeaponCharacteristic.AttackDistance, 2);

        private void Shoot()
        {
            CreateBullet().Forget();

            _canAttack = false;

            _speedAttackTween?.Kill();
            _speedAttackTween = DOVirtual.DelayedCall(WeaponCharacteristic.FireInterval, SetCanAttack);

            ReduceClip();

            if (_clipCount <= 0)
            {
                _rechargeTimeTween?.Kill();
                _rechargeTimeTween = DOVirtual.DelayedCall(WeaponCharacteristic.RechargeTime, ReloadClip);
            }
        }

        private async UniTaskVoid CreateBullet()
        {
            int damage = SetDamage();
            
            for (int i = 0; i < Weapon.SpawnPoints.Length; i++)
            {
                Vector3 normalized = Weapon.SpawnPoints[i].forward.normalized;
                Vector3 direction = new Vector3(normalized.x, 0f, normalized.z) * WeaponCharacteristic.ForceBullet;
            
                await WeaponFactory.CreateProjectile(Weapon.ProjectileType, Weapon.SpawnPoints[i], CalculateCriticalDamage(damage), direction);
            }
        }

        private int CalculateCriticalDamage(int damage)
        {
            bool isCriticalDamage = WeaponCharacteristic.CriticalChance > UnityEngine.Random.Range(0, 100);

            if (isCriticalDamage)
            {
                return Mathf.RoundToInt(damage * WeaponCharacteristic.CriticalMultiplier);
            }

            return damage;
        }
        
        void IDisposable.Dispose()
        {
            _speedAttackTween?.Kill();
            _rechargeTimeTween?.Kill();
        }
    }
}