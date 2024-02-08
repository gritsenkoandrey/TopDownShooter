using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders.Weapon
{
    public sealed class WeaponUnitBuilder : BaseWeaponBuilder
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly IEffectFactory _effectFactory;

        public WeaponUnitBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic, IEffectFactory effectFactory) 
            : base(weaponFactory, weaponCharacteristic, effectFactory)
        {
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponCharacteristic;
            _effectFactory = effectFactory;
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();
            IWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                currentWeapon = new UnitMeleeWeapon(weapon, _weaponCharacteristic, _effectFactory);
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