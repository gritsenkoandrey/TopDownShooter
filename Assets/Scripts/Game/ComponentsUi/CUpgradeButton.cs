using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CUpgradeButton : EntityComponent<CUpgradeButton>, IProgressReader, IProgressWriter
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _textLevel;
        [SerializeField] private TextMeshProUGUI _textCost;

        public Button BuyButton => _buyButton;
        public UpgradeButtonType UpgradeButtonType { get; set; }
        public int Cost { get; private set; }
        public int BaseCost { get; set; }
        public int Level { get; set; }

        private void UpdateProgress()
        {
            Cost = Level * BaseCost;
            _textLevel.text = $"Level {Level}";
            _textCost.text = $"{Cost}$";
        }

        private void ButtonInteractable(PlayerProgress progress)
        {
            _buyButton.interactable = progress.Money >= Cost;
        }

        public void Read(PlayerProgress progress)
        {
            switch (UpgradeButtonType)
            {
                case UpgradeButtonType.Damage:
                    Level = progress.Stats.Damage;
                    break;
                case UpgradeButtonType.Health:
                    Level = progress.Stats.Health;
                    break;
                case UpgradeButtonType.Speed:
                    Level = progress.Stats.Speed;
                    break;
            }

            UpdateProgress();
            ButtonInteractable(progress);
        }

        public void Write(PlayerProgress progress)
        {
            switch (UpgradeButtonType)
            {
                case UpgradeButtonType.Damage:
                    progress.Stats.Damage = Level;
                    break;
                case UpgradeButtonType.Health:
                    progress.Stats.Health = Level;
                    break;
                case UpgradeButtonType.Speed:
                    progress.Stats.Speed = Level;
                    break;
            }
        }

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}