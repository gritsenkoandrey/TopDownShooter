using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterAnimation : SystemComponent<CCharacter>
    {
        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            component.Animator.OnRun
                .Subscribe(delta =>
                {
                    component.Animator.Animator.SetFloat(Animations.Velocity, delta);
                })
                .AddTo(component.LifetimeDisposable);

            component.Animator.OnAttack
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetTrigger(Animations.Shoot);
                    component.Animator.Animator.SetFloat(Animations.ShootBlend, Random.Range(0, 2));
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