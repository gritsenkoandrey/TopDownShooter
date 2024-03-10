using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopPrice : SystemComponent<CShopPrice>
    {
        private InventoryModel _inventoryModel;
        private ShopModel _shopModel;
        private IProgressService _progressService;

        [Inject]
        private void Construct(InventoryModel inventoryModel, ShopModel shopModel, IProgressService progressService)
        {
            _inventoryModel = inventoryModel;
            _shopModel = shopModel;
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CShopPrice component)
        {
            base.OnEnableComponent(component);

            _inventoryModel.SelectedWeapon
                .Subscribe(weaponType =>
                {
                    if (_shopModel.IsBuy(weaponType))
                    {
                        component.CanvasGroup.alpha = 0f;
                    }
                    else
                    {
                        component.CanvasGroup.alpha = 1f;
                        component.CostText.text = _shopModel.GetCost(weaponType).Trim();
                    }
                })
                .AddTo(component.LifetimeDisposable);
            
            _inventoryModel.SelectedSkin
                .Subscribe(skinType =>
                {
                    if (_shopModel.IsBuy(skinType))
                    {
                        component.CanvasGroup.alpha = 0f;
                    }
                    else
                    {
                        component.CanvasGroup.alpha = 1f;
                        component.CostText.text = _shopModel.GetCost(skinType).Trim();
                    }
                })
                .AddTo(component.LifetimeDisposable);

            _progressService.MoneyData.Data
                .Pairwise()
                .Where(money => money.Previous > money.Current)
                .Subscribe(_ =>
                {
                    component.CanvasGroup.alpha = 0f;
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}