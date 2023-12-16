using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

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

            CreateUpgradeButtons(component);

            component.IsShowUpgradeShop
                .Subscribe(value =>
                {
                    component.Show.SetActive(!value);
                    component.Hide.SetActive(value);
                    component.Root.transform.localScale = value ? Vector3.one : Vector3.zero;
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
        
        private void CreateUpgradeButtons(CUpgradeShop component)
        {
            for (int i = 0; i < component.UpgradeButtonType.Length; i++)
            {
                _uiFactory.CreateUpgradeButton(component.UpgradeButtonType[i], component.Root);
            }
        }

        private TimeSpan Time() => TimeSpan.FromSeconds(0.25f);
    }
}