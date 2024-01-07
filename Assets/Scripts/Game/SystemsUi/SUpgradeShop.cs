using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeShop : SystemComponent<CUpgradeShop>
    {
        private readonly IUIFactory _uiFactory;

        public SUpgradeShop(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        protected override void OnEnableComponent(CUpgradeShop component)
        {
            base.OnEnableComponent(component);

            CreateUpgradeButtons(component).Forget();

            component.IsShowUpgradeShop
                .Subscribe(value =>
                {
                    component.Show.SetActive(!value);
                    component.Hide.SetActive(value);
                    component.Root.SetActive(value);
                })
                .AddTo(component.LifetimeDisposable);

            component.Button
                .OnClickAsObservable()
                .ThrottleFirst(Time())
                .Subscribe(_ =>
                {
                    component.Button.transform.PunchTransform();
                    component.IsShowUpgradeShop.Value = !component.IsShowUpgradeShop.Value;
                })
                .AddTo(component.LifetimeDisposable);
        }
        
        private async UniTaskVoid CreateUpgradeButtons(CUpgradeShop component)
        {
            for (int i = 0; i < component.UpgradeButtonType.Length; i++)
            {
                await _uiFactory.CreateUpgradeButton(component.UpgradeButtonType[i], component.Root.transform);
            }
        }

        private TimeSpan Time() => TimeSpan.FromSeconds(0.25f);
    }
}