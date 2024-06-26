﻿using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using DG.Tweening;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SScreenBloodEffect : SystemComponent<CBloodEffect>
    {
        private LevelModel _levelModel;

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CBloodEffect component)
        {
            base.OnEnableComponent(component);
            
            void SetCanvasAlpha(float alpha) => component.CanvasGroup.alpha = alpha;

            _levelModel.Character.Health.CurrentHealth
                .Pairwise()
                .Where(pair => pair.Previous > pair.Current)
                .Subscribe(_ =>
                {
                    component.BloodTween?.Kill();
                    component.BloodTween = DOVirtual.Float(1f, 0f, 0.75f, SetCanvasAlpha)
                        .SetEase(Ease.Linear)
                        .SetLink(component.gameObject);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}