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

        public virtual void Attack(ITarget target = null)
        {
            NotReadyAttack();
            ReduceClip();
            UpdateFireInterval();
            
            if (ClipIsEmpty())
            {
                UpdateRechargeTime();
            }
        }
        
        public bool CanAttack() => _clipCount > 0 && _canAttack;
        public bool IsDetectThroughObstacle() => WeaponCharacteristic.IsDetectThroughObstacle;
        public float AttackDistance() => _attackDistance;
        public float DetectionDistance() => WeaponCharacteristic.DetectionDistance;
        public float AimingSpeed() => WeaponCharacteristic.Aiming;

        private protected virtual void ReloadClip() => _clipCount = WeaponCharacteristic.ClipCount;
        private protected virtual void ReduceClip() => _clipCount--;
        private protected virtual int GetDamage() => WeaponCharacteristic.Damage;
        private protected virtual void SendCombatLog(ITarget target, int damage) { }
        
        private protected int CalculateCriticalDamage(int damage)
        {
            bool isCriticalDamage = WeaponCharacteristic.CriticalChance > Random.Range(0, 100);

            if (isCriticalDamage)
            {
                return Mathf.RoundToInt(damage * WeaponCharacteristic.CriticalMultiplier);
            }

            return damage;
        }

        private void ReadyAttack() => _canAttack = true;
        private void NotReadyAttack() => _canAttack = false;
        private bool ClipIsEmpty() => _clipCount <= 0;

        private void UpdateRechargeTime()
        {
            _rechargeTimeTween?.Kill();
            _rechargeTimeTween = DOVirtual.DelayedCall(WeaponCharacteristic.RechargeTime, ReloadClip);
        }

        private void UpdateFireInterval()
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