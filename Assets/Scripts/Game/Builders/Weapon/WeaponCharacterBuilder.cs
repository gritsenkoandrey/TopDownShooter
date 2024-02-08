using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.Builders.Weapon
{
    public sealed class WeaponCharacterBuilder : BaseWeaponBuilder
    {
        public WeaponCharacterBuilder(IObjectResolver objectResolver, WeaponCharacteristic weaponCharacteristic) 
            : base(objectResolver, weaponCharacteristic)
        {
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();

            BaseWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                currentWeapon = new CharacterMeleeWeapon(weapon, WeaponCharacteristic);
            }
            else
            {
                currentWeapon = new CharacterRangeWeapon(weapon, WeaponCharacteristic);
            }
            
            ObjectResolver.Inject(currentWeapon);
            currentWeapon.Initialize();
            weapon.SetWeapon(currentWeapon);
            
            return weapon;
        }
    }
}