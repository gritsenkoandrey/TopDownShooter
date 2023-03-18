using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Game.Systems
{
    public sealed class SSpawnerZombie : SystemComponent<CSpawnerZombie>
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