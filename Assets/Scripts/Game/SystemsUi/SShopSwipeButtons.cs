using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopSwipeButtons : SystemComponent<CShopSwipeButtons>
    {
        private InventoryModel _inventoryModel;
        private CharacterPreviewModel _characterPreviewModel;

        [Inject]
        private void Construct(InventoryModel inventoryModel, CharacterPreviewModel characterPreviewModel)
        {
            _inventoryModel = inventoryModel;
            _characterPreviewModel = characterPreviewModel;
        }
        
        protected override void OnEnableComponent(CShopSwipeButtons component)
        {
            base.OnEnableComponent(component);

            SubscribeOnClickButtons(component);
        }
        
        private void SubscribeOnClickButtons(CShopSwipeButtons component)
        {
            component.LeftButton
                .OnClickAsObservable()
                .ThrottleFirst(DelayClick())
                .Subscribe(_ => ClickLeftButton(component).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.RightButton
                .OnClickAsObservable()
                .ThrottleFirst(DelayClick())
                .Subscribe(_ => ClickRightButton(component).Forget())
                .AddTo(component.LifetimeDisposable);
        }

        private async UniTaskVoid ClickRightButton(CShopSwipeButtons component)
        {
            component.RightButton.transform.PunchTransform();
            component.RightButton.interactable = false;

            switch (_characterPreviewModel.State.Value)
            {
                case PreviewState.BuyWeapon:
                    await ShowWeaponFade(1f, 2f).AsyncWaitForCompletion().AsUniTask();
                    TurnRightWeapon();
                    await ShowWeaponFade(2f, 1f).AsyncWaitForCompletion().AsUniTask();
                    break;
                case PreviewState.BuySkin:
                    await ShowEquipmentFade(1f, 2f).AsyncWaitForCompletion().AsUniTask();
                    TurnRightSkin();
                    await ShowEquipmentFade(2f, 1f).AsyncWaitForCompletion().AsUniTask();
                    break;
            }

            component.RightButton.interactable = true;
        }

        private async UniTaskVoid ClickLeftButton(CShopSwipeButtons component)
        {
            component.LeftButton.transform.PunchTransform();
            component.LeftButton.interactable = false;

            switch (_characterPreviewModel.State.Value)
            {
                case PreviewState.BuyWeapon:
                    await ShowWeaponFade(1f, 2f).AsyncWaitForCompletion().AsUniTask();
                    TurnLeftWeapon();
                    await ShowWeaponFade(2f, 1f).AsyncWaitForCompletion().AsUniTask();
                    break;
                case PreviewState.BuySkin:
                    await ShowEquipmentFade(1f, 2f).AsyncWaitForCompletion().AsUniTask();
                    TurnLeftSkin();
                    await ShowEquipmentFade(2f, 1f).AsyncWaitForCompletion().AsUniTask();
                    break;
            }

            component.LeftButton.interactable = true;
        }

        private void TurnLeftWeapon()
        {
            CCharacterPreviewModel component = _characterPreviewModel.CharacterPreview.CharacterPreviewModel;
            
            int index = _inventoryModel.IndexWeapon.Value;

            index--;

            if (index < 0)
            {
                index = component.Weapons.Length - 1;
            }
            
            _inventoryModel.IndexWeapon.Value = index;
        }
        
        private void TurnRightWeapon()
        {
            CCharacterPreviewModel component = _characterPreviewModel.CharacterPreview.CharacterPreviewModel;

            int index = _inventoryModel.IndexWeapon.Value;

            index++;

            if (index > component.Weapons.Length - 1)
            {
                index = 0;
            }

            _inventoryModel.IndexWeapon.Value = index;
        }

        private void TurnLeftSkin()
        {
            CCharacterPreviewModel component = _characterPreviewModel.CharacterPreview.CharacterPreviewModel;

            int index = _inventoryModel.IndexSkin.Value;

            index--;

            if (index < 0)
            {
                index = component.Skins.Length - 1;
            }

            _inventoryModel.IndexSkin.Value = index;
        }

        private void TurnRightSkin()
        {
            CCharacterPreviewModel component = _characterPreviewModel.CharacterPreview.CharacterPreviewModel;

            int index = _inventoryModel.IndexSkin.Value;

            index++;

            if (index > component.Skins.Length - 1)
            {
                index = 0;
            }
            
            _inventoryModel.IndexSkin.Value = index;
        }

        private Tween ShowEquipmentFade(float from, float to)
        {
            CCharacterPreviewModel model = _characterPreviewModel.CharacterPreview.CharacterPreviewModel;

            void EquipmentFade(float value)
            {
                for (int i = 0; i < model.EquipmentMaterials.Length; i++)
                {
                    model.EquipmentMaterials[i].SetFloat(Shaders.Fade, value);
                }
            }
            
            return DOVirtual.Float(from, to, 0.25f, EquipmentFade).SetEase(Ease.Linear).SetLink(model.gameObject);
        }
        
        private Tween ShowWeaponFade(float from, float to)
        {
            CCharacterPreviewModel model = _characterPreviewModel.CharacterPreview.CharacterPreviewModel;

            void WeaponFade(float value)
            {
                for (int i = 0; i < model.WeaponMaterials.Length; i++)
                {
                    model.WeaponMaterials[i].SetFloat(Shaders.Fade, value);
                }
            }

            return DOVirtual.Float(from, to, 0.25f, WeaponFade).SetEase(Ease.Linear).SetLink(model.gameObject);
        }
        
        private TimeSpan DelayClick() => TimeSpan.FromSeconds(ButtonSettings.DelayClick);
    }
}