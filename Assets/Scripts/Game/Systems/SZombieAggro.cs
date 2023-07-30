using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieAggro : SystemComponent<CZombie>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.Health.Health
                .Pairwise()
                .Where(pair => pair.Current < pair.Previous)
                .Subscribe(_ => component.IsAggro = true)
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CZombie component)
        {
            base.OnDisableComponent(component);
        }
    }
}