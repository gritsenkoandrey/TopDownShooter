using System;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletLifeTime : SystemComponent<CBullet>
    {
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

        protected override void OnEnableComponent(CBullet component)
        {
            base.OnEnableComponent(component);

            Observable
                .Interval(TimeSpan.FromSeconds(5f))
                .First()
                .Subscribe(_ => UnityEngine.Object.Destroy(component.Object))
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CBullet component)
        {
            base.OnDisableComponent(component);
        }
    }
}