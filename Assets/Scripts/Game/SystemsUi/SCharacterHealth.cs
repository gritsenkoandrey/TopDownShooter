using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterHealth : SystemComponent<CCharacterHealth>
    {
        private readonly IGameFactory _gameFactory;

        public SCharacterHealth(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        protected override void OnEnableComponent(CCharacterHealth component)
        {
            base.OnEnableComponent(component);

            _gameFactory.Character.Health.CurrentHealth
                .Subscribe(health =>
                {
                    component.Text.text = _gameFactory.Character.Health.ToString();
                    component.Fill.fillAmount = Mathematics.Remap(0, _gameFactory.Character.Health.MaxHealth, 0, 1, health);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}