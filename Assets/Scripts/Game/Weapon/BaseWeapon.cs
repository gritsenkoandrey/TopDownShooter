using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseWeapon : IWeapon
    {
        private protected CWeapon Weapon;
        private protected WeaponCharacteristic WeaponCharacteristic;

        private int _clipCount;
        private bool _canAttack;
        private float _rechargeDelay;
        private float _fireIntervalDelay;
        
        protected float AttackDistance;

        protected BaseWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) { }

        public virtual void Initialize()
        {
            AttackDistance = Mathf.Pow(WeaponCharacteristic.AttackDistance, 2);
            
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
        
        bool IWeapon.CanAttack() => _clipCount > 0 && _canAttack;
        bool IWeapon.IsDetectThroughObstacle() => WeaponCharacteristic.IsDetectThroughObstacle;
        float IWeapon.AttackDistance() => AttackDistance;
        float IWeapon.DetectionDistance() => WeaponCharacteristic.DetectionDistance;
        float IWeapon.AimingSpeed() => WeaponCharacteristic.Aiming;
        void IWeapon.Execute()
        {
            if (ClipIsEmpty())
            {
                _rechargeDelay -= Time.deltaTime;

                if (_rechargeDelay < 0f)
                {
                    ReloadClip();
                }
            }

            if (_canAttack == false)
            {
                _fireIntervalDelay -= Time.deltaTime;

                if (_fireIntervalDelay < 0f)
                {
                    ReadyAttack();
                }
            }
        }
        
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
        private void UpdateRechargeTime() => _rechargeDelay = WeaponCharacteristic.RechargeTime;
        private void UpdateFireInterval() => _fireIntervalDelay = WeaponCharacteristic.FireInterval;

        public virtual void Dispose()
        {
        }
    }
}