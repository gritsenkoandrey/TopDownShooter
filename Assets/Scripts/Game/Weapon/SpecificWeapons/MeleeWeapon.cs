using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Progress;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public abstract class MeleeWeapon : BaseWeapon, IWeapon
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly CWeapon _weapon;

        private float _attackDistance;
        private bool _canAttack;

        protected MeleeWeapon(CWeapon weapon, IWeaponFactory weaponFactory, IProgressService progressService, WeaponType weaponType) 
            : base(weaponFactory, progressService, weaponType)
        {
            _weapon = weapon;
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponFactory.GetWeaponCharacteristic(weaponType);
            
            SetCanAttack();
            SetAttackDistance();
        }

        void IWeapon.Attack()
        {
            _canAttack = false;

            DOVirtual.DelayedCall(_weaponCharacteristic.SpeedAttack, SetCanAttack);
        }

        bool IWeapon.CanAttack() => _canAttack;
        bool IWeapon.IsDetectThroughObstacle() => true;
        float IWeapon.AttackDistance() => _attackDistance;

        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(_weaponCharacteristic.AttackDistance, 2);
    }
}