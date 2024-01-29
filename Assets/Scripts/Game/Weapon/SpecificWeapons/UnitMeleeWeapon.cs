using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class UnitMeleeWeapon : MeleeWeapon
    {
        public UnitMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
            ReloadClip();
        }
    }
}