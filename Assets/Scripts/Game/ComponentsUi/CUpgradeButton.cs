using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.Utils;
using TMPro;
using UniRx;
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
        public UpgradeButtonType UpgradeButtonType { get; private set; }
        public int Cost { get; private set; }

        private int _baseCost;

        public IReactiveProperty<bool> IsInit { get; } = new ReactiveProperty<bool>(false);

        public void Init(UpgradeButtonData data)
        {
            UpgradeButtonType = data.UpgradeButtonType;
            _baseCost = data.BaseCost;
            IsInit.Value = true;
        }

        public void UpdateData(int money, int level)
        {
            Cost = level * _baseCost;
            _textLevel.text = string.Format(FormatText.Level, level.ToString());
            _textCost.text = string.Format(FormatText.Cost, Cost.Trim());
            _buyButton.interactable = money >= Cost;
        }
    }
}