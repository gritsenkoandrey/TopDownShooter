using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeButton : SystemComponent<CUpgradeButton>
    {
        private IProgressService _progressService;

        private const float DelayClick = 0.25f;

        [Inject]
        private void Construct(IProgressService progressService)
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
                    _progressService.StatsData.Data.Value.Data[component.UpgradeButtonType]++;
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnUpdateButton(CUpgradeButton component)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(data => data.Data[component.UpgradeButtonType])
                .DelaySubscription(Time())
                .Subscribe(level => UpdateButton(component, level))
                .AddTo(component.LifetimeDisposable);
        }

        private TimeSpan Time() => TimeSpan.FromSeconds(DelayClick);

        private void UpdateButton(CUpgradeButton component, int level)
        {
            component.SetCost(level * component.BaseCost);
            component.TextLevel.text = $"Level {level}";
            component.TextCost.text = $"{component.Cost.Trim()}$";
            
            Entities.Foreach(SetButtonInteractable);
        }

        private void SetButtonInteractable(CUpgradeButton button)
        {
            button.BuyButton.interactable = _progressService.MoneyData.Data.Value >= button.Cost;
        }
    }
}