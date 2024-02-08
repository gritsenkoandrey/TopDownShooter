using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.StaticData.Data;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseWeapon : IWeapon
    {
        private protected CWeapon Weapon;
        private protected WeaponCharacteristic WeaponCharacteristic;

        private int _clipCount;
        private bool _canAttack;
        private float _attackDistance;

        private Tween _fireIntervalTween;
        private Tween _rechargeTimeTween;

        protected BaseWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) { }

        public virtual void Initialize()
        {
            _attackDistance = Mathf.Pow(WeaponCharacteristic.AttackDistance, 2);
            
            ReadyAttack();
            ReloadClip();
        }

        public virtual void Attack(ITarget target = null) { }
        public bool CanAttack() => _clipCount > 0 && _canAttack;
        public bool IsDetectThroughObstacle() => WeaponCharacteristic.IsDetectThroughObstacle;
        public float AttackDistance() => _attackDistance;
        public float DetectionDistance() => WeaponCharacteristic.DetectionDistance;
        
        private protected virtual void ReloadClip() => _clipCount = WeaponCharacteristic.ClipCount;
        private protected virtual void ReduceClip() => _clipCount--;
        private protected virtual int GetDamage(ITarget target) => WeaponCharacteristic.Damage;

        private void ReadyAttack() => _canAttack = true;
        
        private protected void NotReadyAttack() => _canAttack = false;
        private protected bool ClipIsEmpty() => _clipCount <= 0;

        private protected void UpdateRechargeTime()
        {
            _rechargeTimeTween?.Kill();
            _rechargeTimeTween = DOVirtual.DelayedCall(WeaponCharacteristic.RechargeTime, ReloadClip);
        }

        private protected void UpdateFireInterval()
        {
            _fireIntervalTween?.Kill();
            _fireIntervalTween = DOVirtual.DelayedCall(WeaponCharacteristic.FireInterval, ReadyAttack);
        }

        public virtual void Dispose()
        {
            _fireIntervalTween?.Kill();
            _rechargeTimeTween?.Kill();
        }
    }
}