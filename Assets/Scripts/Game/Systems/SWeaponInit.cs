using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Game.Weapon.SpecificWeapons;

namespace CodeBase.Game.Systems
{
    public sealed class SWeaponInit : SystemComponent<CWeapon>
    {
        private readonly IWeaponFactory _weaponFactory;

        public SWeaponInit(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }

        protected override void OnEnableComponent(CWeapon component)
        {
            base.OnEnableComponent(component);

            component.Weapon ??= _weaponFactory.GetWeapon(component, component.WeaponType) as RangeWeapon;
        }
    }
}