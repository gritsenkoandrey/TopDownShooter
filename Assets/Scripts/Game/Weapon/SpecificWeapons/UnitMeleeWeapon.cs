using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.StaticData.Data;

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