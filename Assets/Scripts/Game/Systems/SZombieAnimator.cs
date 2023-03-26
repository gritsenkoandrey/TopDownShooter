using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieAnimator : SystemComponent<CZombie>
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

            foreach (CZombie enemy in Entities)
            {
                enemy.Animator.UpdateAnimator.Execute(enemy.Agent.velocity.sqrMagnitude);
            }
        }

        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .Subscribe(health =>
                {
                    if (health > 0)
                    {
                        component.Animator.Animator.SetTrigger(Animations.Hit);
                    }
                    else
                    {
                        component.Animator.Animator.SetFloat(Animations.DeathBlend, Random.Range(0, 5));
                        component.Animator.Animator.SetTrigger(Animations.Death);
                    }
                })
                .AddTo(component.LifetimeDisposable);

            component.Melee.Attack
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetTrigger(Animations.Shoot);
                    component.Animator.Animator.SetFloat(Animations.ShootBlend, Random.Range(0, 2));
                })
                .AddTo(component.LifetimeDisposable);

            component.Animator.UpdateAnimator
                .Subscribe(velocity =>
                {
                    component.Animator.Animator.SetFloat(Animations.Velocity, velocity, 0.05f, Time.deltaTime);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CZombie component)
        {
            base.OnDisableComponent(component);
        }
    }
}