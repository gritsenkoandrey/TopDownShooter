using CodeBase.ECSCore;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShop : EntityComponent<CShop>
    {
        [SerializeField] private CShopBuyButton _buyButton;
        [SerializeField] private CShopElements _shopElements;
        [SerializeField] private CShopSwipeButtons _swipeButtons;
        [SerializeField] private CShopUpgradeWindow _upgradeWindow;
        [SerializeField] private CShopTaskProvider _taskProvider;
        [SerializeField] private Button _startButton;

        public CShopBuyButton BuyButton => _buyButton;
        public CShopElements ShopElements => _shopElements;
        public CShopSwipeButtons SwipeButtons => _swipeButtons;
        public CShopUpgradeWindow UpgradeWindow => _upgradeWindow;
        public CShopTaskProvider TaskProvider => _taskProvider;
        public Button StartButton => _startButton;
    }
}