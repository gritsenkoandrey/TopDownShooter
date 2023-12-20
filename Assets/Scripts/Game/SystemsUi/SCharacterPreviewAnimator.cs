using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewAnimator : SystemComponent<CCharacterPreviewAnimator>
    {
        protected override void OnEnableComponent(CCharacterPreviewAnimator component)
        {
            base.OnEnableComponent(component);
            
            component.StartAnimation
                .Subscribe(_ =>
                {
                    component.Animator.SetFloat(Animations.PreviewBlend, Random.Range(0, 4));
                    component.Animator.SetTrigger(Animations.Preview);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}