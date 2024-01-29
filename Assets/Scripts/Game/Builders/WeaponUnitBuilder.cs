using CodeBase.Game.Components;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.Models;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class WeaponUnitBuilder : BaseWeaponBuilder
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponCharacteristic _weaponCharacteristic;

        public WeaponUnitBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic) 
            : base(weaponFactory, weaponCharacteristic)
        {
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponCharacteristic;
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();
            IWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                currentWeapon = new UnitMeleeWeapon(weapon, _weaponCharacteristic);
            }
            else
            {
                currentWeapon = new UnitRangeWeapon(weapon, _weaponFactory, _weaponCharacteristic);
            }
            
            weapon.SetWeapon(currentWeapon);
            return weapon;
        }
    }
}