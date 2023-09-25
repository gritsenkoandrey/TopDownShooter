using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterSpawner : SystemComponent<CCharacterSpawner>
    {
        private readonly IGameFactory _gameFactory;

        public SCharacterSpawner(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CCharacterSpawner component)
        {
            base.OnEnableComponent(component);

            ICharacter character = _gameFactory.CreateCharacter(component.Position, component.transform.parent);

            _gameFactory.Enemies
                .ObserveAdd()
                .Subscribe(enemy =>
                {
                    enemy.Value.Target.SetValueAndForceNotify(character);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacterSpawner component)
        {
            base.OnDisableComponent(component);
        }
    }
}