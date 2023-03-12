using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SAttack : SystemComponent<CAttack>
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

        protected override void OnEnableComponent(CAttack component)
        {
            base.OnEnableComponent(component);

            component.Attack
                .Subscribe(health =>
                {
                    health.Health.Health -= component.Damage;

                    health.Health.Hit.Execute();
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CAttack component)
        {
            base.OnDisableComponent(component);
        }
    }
}