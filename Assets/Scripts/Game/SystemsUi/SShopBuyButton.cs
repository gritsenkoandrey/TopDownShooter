using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
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
        private IProgressService _progressService;
        
        private const float DelayClick = 0.25f;

        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel, ShopModel shopModel, 
            InventoryModel inventoryModel, IProgressService progressService)
        {
            _characterPreviewModel = characterPreviewModel;
            _shopModel = shopModel;
            _inventoryModel = inventoryModel;
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CShopBuyButton component)
        {
            base.OnEnableComponent(component);

            component.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(DelayClick))
                .Subscribe(_ =>
                {
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
                    
                    component.Button.transform.PunchTransform();
                })
                .AddTo(component.LifetimeDisposable);

            _inventoryModel.SelectedWeapon
                .Subscribe(weaponType =>
                {
                    if (_shopModel.IsBuy(weaponType))
                    {
                        SelectedState(component);
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
                        SelectedState(component);
                    }
                    else
                    {
                        BuyState(component, _shopModel.CanBuy(skinType));
                    }
                })
                .AddTo(component.LifetimeDisposable);

            _progressService.MoneyData.Data
                .Pairwise()
                .Where(money => money.Previous > money.Current)
                .Subscribe(_ => SelectedState(component))
                .AddTo(component.LifetimeDisposable);
        }

        private void SelectedState(CShopBuyButton component)
        {
            component.Text.text = "SELECT";
            component.Button.interactable = true;
        }

        private void BuyState(CShopBuyButton component, bool canBuy)
        {
            component.Text.text = "BUY";
            component.Button.interactable = canBuy;
        }
    }
}