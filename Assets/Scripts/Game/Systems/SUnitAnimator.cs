using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SUnitAnimator : SystemComponent<CUnit>
    {
        protected override void OnEnableComponent(CUnit component)
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
                })
                .AddTo(component.LifetimeDisposable);

            component.Animator.OnDeath
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetTrigger(Animations.Death);
                })
                .AddTo(component.LifetimeDisposable);
            
            component.Animator.OnVictory
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetTrigger(Animations.Victory);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}