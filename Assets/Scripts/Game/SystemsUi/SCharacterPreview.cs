using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreview : SystemComponent<CCharacterPreview>
    {
        private InventoryModel _inventoryModel;
        private CharacterPreviewModel _characterPreviewModel;

        [Inject]
        private void Construct(InventoryModel inventoryModel, CharacterPreviewModel characterPreviewModel)
        {
            _inventoryModel = inventoryModel;
            _characterPreviewModel = characterPreviewModel;
        }
        
        protected override void OnEnableComponent(CCharacterPreview component)
        {
            base.OnEnableComponent(component);
            
            SubscribeOnChangeEquipment(component);
            SubscribeOnStartPreviewState(component);
        }

        protected override void OnDisableComponent(CCharacterPreview component)
        {
            base.OnDisableComponent(component);
            
            component.Camera.targetTexture.Release();
            component.Camera.targetTexture = null;
        }

        private void SubscribeOnChangeEquipment(CCharacterPreview component)
        {
            _inventoryModel.IndexWeapon.Value = _inventoryModel.GetWeaponIndex();
            _inventoryModel.IndexSkin.Value = _inventoryModel.GetSkinIndex();
            
            _inventoryModel.IndexWeapon
                .Subscribe(index =>
                {
                    SetWeapon(component.CharacterPreviewModel, index);
                    SetAnimatorController(component, index);
                })
                .AddTo(component.LifetimeDisposable);

            _inventoryModel.IndexSkin
                .Subscribe(index =>
                {
                    SetEquipment(component.CharacterPreviewModel, index);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnStartPreviewState(CCharacterPreview component)
        {
            _characterPreviewModel.State
                .Where(state => state == PreviewState.Start)
                .Subscribe(_ =>
                {
                    _inventoryModel.IndexWeapon.Value = _inventoryModel.GetWeaponIndex();
                    _inventoryModel.IndexSkin.Value = _inventoryModel.GetSkinIndex();
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SetWeapon(CCharacterPreviewModel component, int index)
        {
            for (int i = 0; i < component.Weapons.Length; i++)
            {
                component.Weapons[i].Weapon.SetActive(i == index);
            }
            
            _inventoryModel.SelectedWeapon.Value = component.Weapons[index].WeaponType;
        }

        private void SetEquipment(CCharacterPreviewModel component, int index)
        {
            for (int i = 0; i < component.Skins.Length; i++)
            for (int j = 0; j < component.Skins[i].Data.Visual.Length; j++)
            {
                component.Skins[i].Data.Visual[j].SetActive(i == index);
            }
            
            _inventoryModel.SelectedSkin.Value = component.Skins[index].Type;
        }

        private void SetAnimatorController(CCharacterPreview component, int index)
        {
            component.CharacterPreviewAnimator.Animator.runtimeAnimatorController = 
                component.CharacterPreviewModel.Weapons[index].Weapon.RuntimeAnimatorController;
        }
    }
}