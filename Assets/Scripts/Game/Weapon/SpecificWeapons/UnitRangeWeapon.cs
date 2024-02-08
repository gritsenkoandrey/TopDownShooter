using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.StaticData.Data;
using VContainer;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class UnitRangeWeapon : BaseRangeWeapon
    {
        public UnitRangeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
            Weapon = weapon;
            WeaponCharacteristic = weaponCharacteristic;
        }

        [Inject]
        private void Construct(IWeaponFactory weaponFactory)
        {
            WeaponFactory = weaponFactory;
        }
    }
}