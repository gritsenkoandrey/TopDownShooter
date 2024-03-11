using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopBuyButton : SystemComponent<CShopBuyButton>
    {
        private CharacterPreviewModel _characterPreviewModel;
        private ShopModel _shopModel;
        private InventoryModel _inventoryModel;
        
        private const float DelayClick = 0.25f;

        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel, ShopModel shopModel, 
            InventoryModel inventoryModel)
        {
            _characterPreviewModel = characterPreviewModel;
            _shopModel = shopModel;
            _inventoryModel = inventoryModel;
        }

        protected override void OnEnableComponent(CShopBuyButton component)
        {
            base.OnEnableComponent(component);

            SubscribeOnBuyInventory(component);
            SubscribeOnSelectInventory(component);
        }

        private void SubscribeOnBuyInventory(CShopBuyButton component)
        {
            component.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(DelayClick))
                .Subscribe(_ =>
                {
                    component.Button.transform.PunchTransform();

                    switch (_characterPreviewModel.State.Value)
                    {
                        case PreviewState.BuyWeapon:
                            _inventoryModel.SetWeaponIndex(_inventoryModel.IndexWeapon.Value);
                            _shopModel.Buy(_inventoryModel.SelectedWeapon.Value);
                            break;
                        case PreviewState.BuySkin:
                            _inventoryModel.SetSkinIndex(_inventoryModel.IndexSkin.Value);
                            _shopModel.Buy(_inventoryModel.SelectedSkin.Value);
                            break;
                    }

                    SelectedState(component);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnSelectInventory(CShopBuyButton component)
        {
            _inventoryModel.SelectedWeapon
                .Subscribe(weaponType =>
                {
                    if (_shopModel.IsBuy(weaponType))
                    {
                        if (_inventoryModel.IndexWeapon.Value == _inventoryModel.GetWeaponIndex())
                        {
                            SelectedState(component);
                        }
                        else
                        {
                            SelectState(component);
                        }
                    }
                    else
                    {
                        BuyState(component, _shopModel.CanBuy(weaponType));
                    }
                })
                .AddTo(component.LifetimeDisposable);

            _inventoryModel.SelectedSkin
                .Subscribe(skinType =>
                {
                    if (_shopModel.IsBuy(skinType))
                    {
                        if (_inventoryModel.IndexSkin.Value == _inventoryModel.GetSkinIndex())
                        {
                            SelectedState(component);
                        }
                        else
                        {
                            SelectState(component);
                        }
                    }
                    else
                    {
                        BuyState(component, _shopModel.CanBuy(skinType));
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SelectState(CShopBuyButton component)
        {
            component.Text.text = "SELECT";
            component.Button.interactable = true;
            component.Button.image.color = component.SelectColor;
        }

        private void BuyState(CShopBuyButton component, bool canBuy)
        {
            component.Text.text = "BUY";
            component.Button.interactable = canBuy;
            component.Button.image.color = component.BuyColor;
        }

        private void SelectedState(CShopBuyButton component)
        {
            component.Text.text = "SELECTED";
            component.Button.interactable = false;
            component.Button.image.color = component.SelectedColor;
        }
    }
}