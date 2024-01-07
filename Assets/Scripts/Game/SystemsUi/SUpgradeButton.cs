using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeButton : SystemComponent<CUpgradeButton>
    {
        private readonly IProgressService _progressService;

        public SUpgradeButton(IProgressService progressService)
        {
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CUpgradeButton component)
        {
            base.OnEnableComponent(component);

            SubscribeOnBuyButtonClick(component);
            SubscribeOnUpdateButton(component);
        }

        private void SubscribeOnBuyButtonClick(CUpgradeButton component)
        {
            component.BuyButton
                .OnClickAsObservable()
                .ThrottleFirst(Time())
                .Subscribe(_ =>
                {
                    component.BuyButton.transform.PunchTransform();
                    
                    _progressService.MoneyData.Data.Value -= component.Cost;
                    _progressService.StatsData.Data.Value[component.UpgradeButtonType]++;
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnUpdateButton(CUpgradeButton component)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(data => data[component.UpgradeButtonType])
                .Subscribe(level => UpdateButton(component, level))
                .AddTo(component.LifetimeDisposable);
        }

        private TimeSpan Time() => TimeSpan.FromSeconds(0.25f);

        private void UpdateButton(CUpgradeButton component, int level)
        {
            component.Cost = level * component.BaseCost;
            component.TextLevel.text = $"Level {level}";
            component.TextCost.text = $"{component.Cost}$";
            component.BuyButton.interactable = _progressService.MoneyData.Data.Value >= component.Cost;
        }
    }
}