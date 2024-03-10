using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
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
                .Subscribe(_ =>
                {
                    component.LeftButton.transform.PunchTransform();

                    switch (_characterPreviewModel.State.Value)
                    {
                        case PreviewState.BuyWeapon:
                            TurnLeftWeapon();
                            break;
                        case PreviewState.BuySkin:
                            TurnLeftSkin();
                            break;
                    }
                })
                .AddTo(component.LifetimeDisposable);
            
            component.RightButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.RightButton.transform.PunchTransform();

                    switch (_characterPreviewModel.State.Value)
                    {
                        case PreviewState.BuyWeapon:
                            TurnRightWeapon();
                            break;
                        case PreviewState.BuySkin:
                            TurnRightSkin();
                            break;
                    }
                })
                .AddTo(component.LifetimeDisposable);
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
    }
}