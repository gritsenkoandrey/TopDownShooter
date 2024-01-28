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
        private readonly DamageCombatLog _damageCombatLog;

        public WeaponUnitBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic, DamageCombatLog damageCombatLog) 
            : base(weaponFactory, weaponCharacteristic, damageCombatLog)
        {
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponCharacteristic;
            _damageCombatLog = damageCombatLog;
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();
            IWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                currentWeapon = new UnitMeleeWeapon(weapon, _weaponCharacteristic, _damageCombatLog);
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