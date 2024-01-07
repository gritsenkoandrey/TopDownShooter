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
        public UpgradeButtonType UpgradeButtonType { get; set; }
        public int Cost { get; set; }
        public int BaseCost { get; set; }
    }
}