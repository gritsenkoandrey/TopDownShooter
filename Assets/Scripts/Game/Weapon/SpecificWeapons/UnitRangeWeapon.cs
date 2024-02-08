using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.StaticData.Data;

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