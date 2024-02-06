using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Infrastructure.Factories.Effects;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class UnitMeleeWeapon : MeleeWeapon
    {
        public UnitMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic, IEffectFactory effectFactory) 
            : base(weapon, weaponCharacteristic, effectFactory)
        {
            ReloadClip();
        }
    }
}