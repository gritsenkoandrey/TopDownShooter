using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using DG.Tweening;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SBloodEffect : SystemComponent<CBloodEffect>
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
            
            _levelModel.Character.Health.CurrentHealth
                .Pairwise()
                .Where(pair => pair.Previous > pair.Current)
                .Subscribe(_ =>
                {
                    component.BloodTween?.Kill();
                    component.BloodTween = DOVirtual.Float(1f, 0f, 0.75f, 
                        alpha => component.CanvasGroup.alpha = alpha);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CBloodEffect component)
        {
            base.OnDisableComponent(component);
            
            component.BloodTween?.Kill();
        }
    }
}