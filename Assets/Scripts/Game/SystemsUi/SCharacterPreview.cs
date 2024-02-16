using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreview : SystemComponent<CCharacterPreview>
    {
        private InventoryModel _inventoryModel;
        private IProgressService _progressService;

        [Inject]
        private void Construct(InventoryModel inventoryModel, IProgressService progressService)
        {
            _inventoryModel = inventoryModel;
            _progressService = progressService;
        }
        
        protected override void OnEnableComponent(CCharacterPreview component)
        {
            base.OnEnableComponent(component);
            
            SubscribeOnChangeEquipment(component);
        }

        protected override void OnDisableComponent(CCharacterPreview component)
        {
            base.OnDisableComponent(component);
            
            component.Camera.targetTexture.Release();
            component.Camera.targetTexture = null;
        }

        private void SubscribeOnChangeEquipment(CCharacterPreview component)
        {
            _progressService.InventoryData.Data.Value
                .ObserveEveryValueChanged(data => data.WeaponIndex)
                .Subscribe(index =>
                {
                    SetWeapon(component.CharacterPreviewModel, index);
                    SetAnimatorController(component, index);
                })
                .AddTo(component.LifetimeDisposable);
            
            _progressService.InventoryData.Data.Value
                .ObserveEveryValueChanged(data => data.EquipmentIndex)
                .Subscribe(index =>
                {
                    SetEquipment(component.CharacterPreviewModel, index);
                })
                .AddTo(component.LifetimeDisposable);
        }
        
        private void SetWeapon(CCharacterPreviewModel component, int index)
        {
            for (int i = 0; i < component.Weapons.Length; i++)
            {
                component.Weapons[i].Weapon.SetActive(i == index);
            }
            
            _inventoryModel.SetSelectedWeapon(component.Weapons[index].WeaponType);
        }

        private void SetEquipment(CCharacterPreviewModel component, int index)
        {
            for (int i = 0; i < component.Heads.Length; i++)
            {
                component.Heads[i].SetActive(i == index);
            }

            for (int i = 0; i < component.Bodies.Length; i++)
            {
                component.Bodies[i].SetActive(i == index);
            }
        }

        private void SetAnimatorController(CCharacterPreview component, int index)
        {
            component.CharacterPreviewAnimator.Animator.runtimeAnimatorController = 
                component.CharacterPreviewModel.Weapons[index].Weapon.RuntimeAnimatorController;
        }
    }
}