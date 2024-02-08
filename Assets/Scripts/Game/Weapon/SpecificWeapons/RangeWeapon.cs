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
    public abstract class RangeWeapon : BaseWeapon, IDisposable
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly CWeapon _weapon;
        private readonly WeaponCharacteristic _weaponCharacteristic;

        private int _clipCount;
        private float _attackDistance;
        private bool _canAttack;

        private Tween _speedAttackTween;
        private Tween _rechargeTimeTween;

        protected RangeWeapon(CWeapon weapon, IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic)
            : base(weapon, weaponCharacteristic)
        {
            _weapon = weapon;
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponCharacteristic;
            
            SetAttackDistance();
            SetCanAttack();
        }

        public void Attack(ITarget target) => Shoot();
        public bool CanAttack() => _clipCount > 0 && _canAttack;
        public bool IsDetectThroughObstacle() => _weaponCharacteristic.IsDetectThroughObstacle;
        public float AttackDistance() => _attackDistance;
        public float DetectionDistance() => _weaponCharacteristic.DetectionDistance;

        private protected virtual void ReloadClip() => _clipCount = _weaponCharacteristic.ClipCount;
        private protected virtual void ReduceClip() => _clipCount--;
        private protected virtual int SetDamage() => _weaponCharacteristic.Damage;

        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(_weaponCharacteristic.AttackDistance, 2);

        private void Shoot()
        {
            CreateBullet().Forget();

            _canAttack = false;

            _speedAttackTween?.Kill();
            _speedAttackTween = DOVirtual.DelayedCall(_weaponCharacteristic.FireInterval, SetCanAttack);

            ReduceClip();

            if (_clipCount <= 0)
            {
                _rechargeTimeTween?.Kill();
                _rechargeTimeTween = DOVirtual.DelayedCall(_weaponCharacteristic.RechargeTime, ReloadClip);
            }
        }

        private async UniTaskVoid CreateBullet()
        {
            int damage = SetDamage();
            
            for (int i = 0; i < _weapon.SpawnPoints.Length; i++)
            {
                Vector3 normalized = _weapon.SpawnPoints[i].forward.normalized;
                Vector3 direction = new Vector3(normalized.x, 0f, normalized.z) * _weaponCharacteristic.ForceBullet;
            
                await _weaponFactory.CreateProjectile(_weapon.ProjectileType, _weapon.SpawnPoints[i], CalculateCriticalDamage(damage), direction);
            }
        }

        private int CalculateCriticalDamage(int damage)
        {
            bool isCriticalDamage = _weaponCharacteristic.CriticalChance > UnityEngine.Random.Range(0, 100);

            if (isCriticalDamage)
            {
                return Mathf.RoundToInt(damage * _weaponCharacteristic.CriticalMultiplier);
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