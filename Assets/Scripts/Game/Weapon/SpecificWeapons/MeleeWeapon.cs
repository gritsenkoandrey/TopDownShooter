using System;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public abstract class MeleeWeapon : BaseWeapon, IWeapon
    {
        private protected IEffectFactory EffectFactory;

        private float _attackDistance;
        private int _clipCount;
        private bool _canAttack;

        private Tween _fireIntervalTween;
        private Tween _checkDistanceTween;
        private Tween _rechargeTimeTween;
        
        private ITarget _target;

        protected MeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
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

        void IWeapon.Attack(ITarget target) => Hit(target);
        bool IWeapon.CanAttack() => _canAttack && _clipCount > 0;
        bool IWeapon.IsDetectThroughObstacle() => WeaponCharacteristic.IsDetectThroughObstacle;
        float IWeapon.AttackDistance() => _attackDistance;
        float IWeapon.DetectionDistance() => WeaponCharacteristic.DetectionDistance;

        private protected virtual void ReloadClip() => _clipCount = WeaponCharacteristic.ClipCount;
        private protected virtual void ReduceClip() => _clipCount--;
        private protected virtual int SetDamage(ITarget target) => WeaponCharacteristic.Damage;

        private void Hit(ITarget target)
        {
            _target = target;
            _canAttack = false;
            
            ReduceClip();

            _fireIntervalTween?.Kill();
            _fireIntervalTween = DOVirtual.DelayedCall(WeaponCharacteristic.FireInterval, SetCanAttack);

            _checkDistanceTween?.Kill();
            _checkDistanceTween = DOVirtual.DelayedCall(WeaponCharacteristic.FireInterval / 5f, CheckDamage);

            if (_clipCount <= 0)
            {
                _rechargeTimeTween?.Kill();
                _rechargeTimeTween = DOVirtual.DelayedCall(WeaponCharacteristic.RechargeTime, ReloadClip);
            }
        }

        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(WeaponCharacteristic.AttackDistance, 2);

        private void CheckDamage()
        {
            for (int i = 0; i < Weapon.SpawnPoints.Length; i++)
            {
                float distance = (Weapon.SpawnPoints[i].position - _target.Position).sqrMagnitude;

                if (distance < _attackDistance && _target.Health.IsAlive)
                {
                    int damage = SetDamage(_target);
                    
                    _target.Health.CurrentHealth.Value -= damage;
                    
                    EffectFactory.CreateEffect(EffectType.Hit, _target.Position).Forget();
                }
            }
        }

        void IDisposable.Dispose()
        {
            _fireIntervalTween?.Kill();
            _checkDistanceTween?.Kill();
        }
    }
}