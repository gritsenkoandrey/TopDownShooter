using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class UnitMeleeWeapon : MeleeWeapon
    {
        public UnitMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic, DamageCombatLog damageCombatLog) 
            : base(weapon, weaponCharacteristic, damageCombatLog)
        {
            ReloadClip();
        }
    }
}