using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.Builders.Weapon
{
    public sealed class WeaponUnitBuilder : BaseWeaponBuilder
    {
        public WeaponUnitBuilder(IObjectResolver objectResolver, WeaponCharacteristic weaponCharacteristic) 
            : base(objectResolver, weaponCharacteristic)
        {
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();
            
            IWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                MeleeWeapon meleeWeapon = new UnitMeleeWeapon(weapon, WeaponCharacteristic);
                currentWeapon = meleeWeapon;
                ObjectResolver.Inject(meleeWeapon);
                meleeWeapon.Initialize();
            }
            else
            {
                RangeWeapon rangeWeapon = new UnitRangeWeapon(weapon, WeaponCharacteristic);
                currentWeapon = rangeWeapon;
                ObjectResolver.Inject(rangeWeapon);
                rangeWeapon.Initialize();
            }
            
            weapon.SetWeapon(currentWeapon);
            
            return weapon;
        }
    }
}