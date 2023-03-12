using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyAnimationController : SystemComponent<CEnemy>
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

            foreach (CEnemy enemy in Entities)
            {
                UpdateAnimation(enemy);
            }
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            component.Health.Hit
                .Subscribe(_ => component.Animator.SetTrigger(Animations.Hit))
                .AddTo(component.LifetimeDisposable);

            component.Attack.Attack
                .Subscribe(_ =>
                {
                    component.Animator.SetTrigger(Animations.Shoot);
                    component.Animator.SetFloat(Animations.ShootBlend, Random.Range(0, 2));
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }

        private void UpdateAnimation(CEnemy enemy)
        {
            enemy.Animator.SetFloat(Animations.Velocity, enemy.Agent.velocity.sqrMagnitude, 0.05f, Time.deltaTime);
        }
    }
}