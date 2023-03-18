using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SHealthProvider : SystemComponent<CHealth>
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

        protected override void OnEnableComponent(CHealth component)
        {
            base.OnEnableComponent(component);

            component.Hit
                .Subscribe(damage =>
                {
                    component.Health -= damage;
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CHealth component)
        {
            base.OnDisableComponent(component);
        }
    }
}