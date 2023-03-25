using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Utils;
using DG.Tweening;
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
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
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
                    DisplayUpgradeShop(component, value);
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

        protected override void OnDisableComponent(CUpgradeShop component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private void DisplayUpgradeShop(CUpgradeShop component, bool value)
        {
            if (value)
            {
                ShowUpgradeShop(component);
            }
            else
            {
                HideUpgradeShop(component);
            }
        }

        private void ShowUpgradeShop(CUpgradeShop component)
        {
            component.Tween?.Kill();
            component.Root.transform.localScale = Vector3.zero;
            component.Tween = component.Root.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
        }
        
        private void HideUpgradeShop(CUpgradeShop component)
        {
            component.Tween?.Kill();
            component.Root.transform.localScale = Vector3.one;
            component.Tween = component.Root.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack);
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