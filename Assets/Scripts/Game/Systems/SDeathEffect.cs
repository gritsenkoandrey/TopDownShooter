using CodeBase.ECSCore;
using CodeBase.Game.Components;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SDeathEffect : SystemComponent<CDeathEffect>
    {
        protected override void OnEnableComponent(CDeathEffect component)
        {
            base.OnEnableComponent(component);

            component.PlayEffect
                .First()
                .Subscribe(_ =>
                {
                    float duration = Random.Range(0.5f, 2f);
                    float scale = Random.Range(1.5f, 2.5f);
                    
                    component.GetEffect()
                        .DOScale(Vector3.one * scale, duration)
                        .From(Vector3.zero)
                        .SetEase(Ease.Linear)
                        .SetLink(component.gameObject);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}