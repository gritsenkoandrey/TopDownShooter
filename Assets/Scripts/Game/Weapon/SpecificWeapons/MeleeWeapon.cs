using System;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Progress;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class MeleeWeapon : BaseWeapon, IWeapon
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly IProgressService _progressService;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly CWeapon _weapon;

        private float _attackDistance;
        private bool _canAttack;

        public MeleeWeapon(CWeapon weapon, IWeaponFactory weaponFactory, IProgressService progressService, WeaponCharacteristic weaponCharacteristic) 
            : base(weaponFactory, progressService)
        {
            _weapon = weapon;
            _weaponFactory = weaponFactory;
            _progressService = progressService;
            _weaponCharacteristic = weaponCharacteristic;
            
            SetCanAttack();
            SetAttackDistance();
        }

        void IWeapon.Attack()
        {
            _canAttack = false;

            DOVirtual.DelayedCall(_weaponCharacteristic.FireInterval, SetCanAttack);
        }

        bool IWeapon.CanAttack() => _canAttack;
        bool IWeapon.IsDetectThroughObstacle() => true;
        float IWeapon.AttackDistance() => _attackDistance;
        void IDisposable.Dispose() { }

        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(_weaponCharacteristic.AttackDistance, 2);
    }
}