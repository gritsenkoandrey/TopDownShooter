using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieMeleeAttack : SystemComponent<CZombie>
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

            component.Melee.OnCheckDamage
                .Subscribe(_ =>
                {
                    float distance = Vector3.Distance(component.Position, component.Target.Value.Position);

                    if (distance > component.Stats.MinDistanceToTarget || !component.Health.IsAlive)
                    {
                        return;
                    }

                    component.Target.Value.Health.Health.Value -= component.Melee.Damage;
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CZombie component)
        {
            base.OnDisableComponent(component);
        }
    }
}