using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.Factories;
using Cysharp.Threading.Tasks;

namespace CodeBase.Game.Systems
{
    public sealed class SWeaponMediator : SystemComponent<CWeaponMediator>
    {
        private readonly IWeaponFactory _weaponFactory;

        public SWeaponMediator(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }

        protected override void OnEnableComponent(CWeaponMediator component)
        {
            base.OnEnableComponent(component);

            CreateWeapon(component).Forget();
        }

        private async UniTaskVoid CreateWeapon(CWeaponMediator component)
        {
            CWeapon weapon = await _weaponFactory.CreateWeapon(WeaponType.Rifle, component.Container);
            
            component.SetWeapon(weapon);
        }
    }
}