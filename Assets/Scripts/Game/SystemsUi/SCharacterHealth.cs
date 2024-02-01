using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterHealth : SystemComponent<CCharacterHealth>
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

            _levelModel.Character.Health.CurrentHealth
                .Subscribe(health =>
                {
                    component.Text.text = _levelModel.Character.Health.ToString();
                    component.Fill.fillAmount = Mathematics.Remap(0, _levelModel.Character.Health.MaxHealth, 0, 1, health);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}