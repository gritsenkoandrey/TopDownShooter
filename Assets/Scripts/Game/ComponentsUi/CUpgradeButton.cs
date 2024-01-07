using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CUpgradeButton : EntityComponent<CUpgradeButton>
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _textLevel;
        [SerializeField] private TextMeshProUGUI _textCost;

        public Button BuyButton => _buyButton;
        public TextMeshProUGUI TextLevel => _textLevel;
        public TextMeshProUGUI TextCost => _textCost;
        public UpgradeButtonType UpgradeButtonType { get; private set; }
        public int Cost { get; private set; }
        public int BaseCost { get; private set; }

        public void SetUpgradeButtonType(UpgradeButtonType type) => UpgradeButtonType = type;
        public void SetCost(int cost) => Cost = cost;
        public void SetBaseCost(int baseCost) => BaseCost = baseCost;
    }
}