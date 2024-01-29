using System;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon.Data;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public abstract class MeleeWeapon : BaseWeapon, IWeapon
    {
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly CWeapon _weapon;

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
            _weapon = weapon;
            _weaponCharacteristic = weaponCharacteristic;

            SetCanAttack();
            SetAttackDistance();
        }

        void IWeapon.Attack(ITarget target) => Hit(target);
        bool IWeapon.CanAttack() => _canAttack && _clipCount > 0;
        bool IWeapon.IsDetectThroughObstacle() => _weaponCharacteristic.IsDetectThroughObstacle;
        float IWeapon.AttackDistance() => _attackDistance;
        float IWeapon.DetectionDistance() => _weaponCharacteristic.DetectionDistance;

        private protected virtual int SetDamage(ITarget target) => _weaponCharacteristic.Damage;
        private protected virtual void ReloadClip() => _clipCount = _weaponCharacteristic.ClipCount;
        private protected virtual void ReduceClip() => _clipCount--;

        private void Hit(ITarget target)
        {
            _target = target;
            _canAttack = false;
            
            ReduceClip();

            _fireIntervalTween?.Kill();
            _fireIntervalTween = DOVirtual.DelayedCall(_weaponCharacteristic.FireInterval, SetCanAttack);

            _checkDistanceTween?.Kill();
            _checkDistanceTween = DOVirtual.DelayedCall(_weaponCharacteristic.FireInterval / 5f, CheckDamage);

            if (_clipCount <= 0)
            {
                _rechargeTimeTween?.Kill();
                _rechargeTimeTween = DOVirtual.DelayedCall(_weaponCharacteristic.RechargeTime, ReloadClip);
            }
        }
        
        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(_weaponCharacteristic.AttackDistance, 2);

        private void CheckDamage()
        {
            for (int i = 0; i < _weapon.SpawnPoints.Length; i++)
            {
                float distance = (_weapon.SpawnPoints[i].position - _target.Position).sqrMagnitude;

                if (distance < _attackDistance && _target.Health.IsAlive)
                {
                    int damage = SetDamage(_target);
                    
                    _target.Health.CurrentHealth.Value -= damage;
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