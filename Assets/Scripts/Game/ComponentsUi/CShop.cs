using CodeBase.ECSCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShop : EntityComponent<CShop>
    {
        [SerializeField] private Button _weaponShopButton;
        [SerializeField] private Button _skinShopButton;
        [SerializeField] private Button _upgradeShopButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private CanvasGroup _shopButtonsCanvasGroup;
        [SerializeField] private TextMeshProUGUI _buyButtonText;

        public Button WeaponShopButton => _weaponShopButton;
        public Button SkinShopButton => _skinShopButton;
        public Button UpgradeShopButton => _upgradeShopButton;
        public Button BackButton => _backButton;
        public Button BuyButton => _buyButton;
        public Button StartButton => _startButton;
        public CanvasGroup ShopButtonsCanvasGroup => _shopButtonsCanvasGroup;
        public TextMeshProUGUI BuyButtonText => _buyButtonText;
    }
}