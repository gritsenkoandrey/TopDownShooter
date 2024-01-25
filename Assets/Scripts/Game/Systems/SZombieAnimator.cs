using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieAnimator : SystemComponent<CZombie>
    {
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.Animator.OnAttack
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetTrigger(Animations.Shoot);
                    component.Animator.Animator.SetFloat(Animations.ShootBlend, Random.Range(0, 2));
                })
                .AddTo(component.LifetimeDisposable);

            component.Animator.OnRun
                .Subscribe(blend =>
                {
                    component.Animator.Animator.SetFloat(Animations.Velocity, 1f);
                    component.Animator.Animator.SetFloat(Animations.RunBlend, blend);
                })
                .AddTo(component.LifetimeDisposable);
            
            component.Animator.OnIdle
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetFloat(Animations.Velocity, 0f);
                })
                .AddTo(component.LifetimeDisposable);
            
            component.Animator.OnDeath
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetFloat(Animations.DeathBlend, Random.Range(0, 5));
                    component.Animator.Animator.SetTrigger(Animations.Death);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}