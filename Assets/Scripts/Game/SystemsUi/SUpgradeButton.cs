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
                .ThrottleFirst(DelayClick())
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
                .DelaySubscription(DelayClick())
                .Subscribe(level => UpdateButton(component, level))
                .AddTo(component.LifetimeDisposable);
        }

        private TimeSpan DelayClick() => TimeSpan.FromSeconds(ButtonSettings.DelayClick);

        private void UpdateButton(CUpgradeButton component, int level)
        {
            component.SetCost(level * component.BaseCost);
            component.TextLevel.text = string.Format(FormatText.Level, level.ToString());
            component.TextCost.text = string.Format(FormatText.Cost, component.Cost.Trim());
            
            Entities.Foreach(SetButtonInteractable);
        }

        private void SetButtonInteractable(CUpgradeButton button)
        {
            button.BuyButton.interactable = _progressService.MoneyData.Data.Value >= button.Cost;
        }
    }
}