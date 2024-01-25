using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class UnitRangeWeapon : RangeWeapon, IWeapon
    {
        public UnitRangeWeapon(CWeapon weapon, IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponFactory, weaponCharacteristic)
        {
            ReloadClip();
        }
    }
}