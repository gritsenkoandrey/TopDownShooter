using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyInitialize : SystemComponent<CEnemy>
    {
        private IGameFactory _gameFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);
            
            _gameFactory.CurrentCharacter.Enemies.Add(component);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);

            _gameFactory.CurrentCharacter.Enemies.Remove(component);
        }
    }
}