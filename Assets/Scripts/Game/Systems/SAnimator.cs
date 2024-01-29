using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SAnimator : SystemComponent<CAnimator>
    {
        protected override void OnEnableComponent(CAnimator component)
        {
            base.OnEnableComponent(component);

            component.OnRun
                .Subscribe(delta => component.Animator.SetFloat(Animations.Velocity, delta))
                .AddTo(component.LifetimeDisposable);

            component.OnAttack
                .Subscribe(_ => component.Animator.SetTrigger(Animations.Shoot))
                .AddTo(component.LifetimeDisposable);

            component.OnDeath
                .Subscribe(_ => component.Animator.SetTrigger(Animations.Death))
                .AddTo(component.LifetimeDisposable);
            
            component.OnVictory
                .Subscribe(_ => component.Animator.SetTrigger(Animations.Victory))
                .AddTo(component.LifetimeDisposable);
        }
    }
}