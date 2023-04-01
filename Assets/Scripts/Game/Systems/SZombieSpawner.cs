using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieSpawner : SystemComponent<CSpawnerZombie>
    {
        private readonly IGameFactory _gameFactory;

        public SZombieSpawner(IGameFactory gameFactory)
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

        protected override void OnEnableComponent(CSpawnerZombie component)
        {
            base.OnEnableComponent(component);

            _gameFactory.CreateZombie(component.ZombieType, component.Position, component.transform.parent);
        }

        protected override void OnDisableComponent(CSpawnerZombie component)
        {
            base.OnDisableComponent(component);
        }
    }
}