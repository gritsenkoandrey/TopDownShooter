using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.StaticData.Data;
using VContainer;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class UnitMeleeWeapon : BaseMeleeWeapon
    {
        public UnitMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
            Weapon = weapon;
            WeaponCharacteristic = weaponCharacteristic;
        }

        [Inject]
        private void Construct(IEffectFactory effectFactory)
        {
            EffectFactory = effectFactory;
        }
    }
}