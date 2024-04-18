using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopUpgradeButton : SystemComponent<CUpgradeButton>
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

            component.IsInit
                .First(isInit => isInit)
                .Subscribe(_ =>
                {
                    SubscribeOnDataChange(component);
                })
                .AddTo(component.LifetimeDisposable);
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

        private void SubscribeOnDataChange(CUpgradeButton component)
        {
            IObservable<int> observable = _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(data => data.Data[component.UpgradeButtonType]);

            _progressService.MoneyData.Data
                .CombineLatest(observable, (money, level) => (money, level))
                .Subscribe(tuple => component.UpdateData(tuple.money, tuple.level))
                .AddTo(component.LifetimeDisposable);
        }

        private TimeSpan DelayClick() => TimeSpan.FromSeconds(ButtonSettings.DelayClick);
    }
}