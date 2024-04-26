using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SScreenVisualCharacterHealth : SystemComponent<CCharacterHealth>
    {
        private LevelModel _levelModel;

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CCharacterHealth component)
        {
            base.OnEnableComponent(component);

            void SetHealth(int health)
            {
                component.Tween?.Kill();
                component.Text.text = _levelModel.Character.Health.ToString();

                float fillAmount = Mathematics.Remap(0, _levelModel.Character.Health.MaxHealth, 0, 1, health);
                    
                component.Fill.fillAmount = fillAmount;
                component.Tween = component.FillLerp.DOFillAmount(fillAmount, 0.25f).SetEase(Ease.Linear);
            }

            _levelModel.Character.Health.CurrentHealth
                .Subscribe(SetHealth)
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacterHealth component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }
    }
}