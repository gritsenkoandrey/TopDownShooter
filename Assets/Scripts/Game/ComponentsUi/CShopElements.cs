using CodeBase.ECSCore;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShopElements : EntityComponent<CShopElements>
    {
        [SerializeField] private Button _weaponShopButton;
        [SerializeField] private Button _skinShopButton;
        [SerializeField] private Button _upgradeShopButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private CanvasGroup _shopButtonsCanvasGroup;

        public Button WeaponShopButton => _weaponShopButton;
        public Button SkinShopButton => _skinShopButton;
        public Button UpgradeShopButton => _upgradeShopButton;
        public Button BackButton => _backButton;
        public CanvasGroup ShopButtonsCanvasGroup => _shopButtonsCanvasGroup;
    }
}