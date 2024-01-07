using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
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
            
            switch (component.UpgradeButtonType)
            {
                case UpgradeButtonType.Damage:
                    SubscribeOnChangeDamage(component);
                    break;
                case UpgradeButtonType.Health:
                    SubscribeOnChangeHealth(component);
                    break;
                case UpgradeButtonType.Speed:
                    SubscribeOnChangeSpeed(component);
                    break;
            }
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

                    switch (component.UpgradeButtonType)
                    {
                        case UpgradeButtonType.Damage:
                            _progressService.StatsData.Data.Value.Damage++;
                            break;
                        case UpgradeButtonType.Health:
                            _progressService.StatsData.Data.Value.Health++;
                            break;
                        case UpgradeButtonType.Speed:
                            _progressService.StatsData.Data.Value.Speed++;
                            break;
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        private TimeSpan Time() => TimeSpan.FromSeconds(0.25f);

        private void SubscribeOnChangeHealth(CUpgradeButton component)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(stats => stats.Health)
                .Subscribe(level => UpdateButton(component, level))
                .AddTo(component.LifetimeDisposable);
        }
        
        private void SubscribeOnChangeDamage(CUpgradeButton component)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(stats => stats.Damage)
                .Subscribe(level => UpdateButton(component, level))
                .AddTo(component.LifetimeDisposable);
        }
        
        private void SubscribeOnChangeSpeed(CUpgradeButton component)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(stats => stats.Speed)
                .Subscribe(level => UpdateButton(component, level))
                .AddTo(component.LifetimeDisposable);
        }

        private void UpdateButton(CUpgradeButton component, int level)
        {
            component.Cost = level * component.BaseCost;
            component.TextLevel.text = $"Level {level}";
            component.TextCost.text = $"{component.Cost}$";
            component.BuyButton.interactable = _progressService.MoneyData.Data.Value >= component.Cost;
        }
    }
}