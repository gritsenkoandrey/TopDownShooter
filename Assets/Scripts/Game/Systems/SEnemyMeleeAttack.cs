using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyMeleeAttack : SystemComponent<CEnemy>
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

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            component.Melee.OnCheckDamage
                .Subscribe(_ =>
                {
                    float distance = Vector3.Distance(component.transform.position, component.Character.Position);

                    if (distance > component.Stats.MinDistanceToTarget || component.Health.Health.Value <= 0)
                    {
                        return;
                    }

                    component.Character.Health.Health.Value -= component.Melee.Damage;
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}