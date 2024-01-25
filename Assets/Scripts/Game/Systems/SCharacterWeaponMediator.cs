using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterWeaponMediator : SystemComponent<CCharacter>
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly InventoryModel _inventoryModel;

        public SCharacterWeaponMediator(IWeaponFactory weaponFactory, InventoryModel inventoryModel)
        {
            _weaponFactory = weaponFactory;
            _inventoryModel = inventoryModel;
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            CreateWeapon(component).Forget();
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
            
            component.WeaponMediator.CurrentWeapon.Weapon?.Dispose();
        }

        private async UniTaskVoid CreateWeapon(CCharacter component)
        {
            CWeapon weapon = await _weaponFactory.CreateCharacterWeapon(_inventoryModel.SelectedWeapon.Value, component.WeaponMediator.Container);
            
            component.WeaponMediator.SetWeapon(weapon);
            component.Animator.Animator.runtimeAnimatorController = weapon.RuntimeAnimatorController;
        }
    }
}