using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieSpawner : SystemComponent<CZombieSpawner>
    {
        private readonly IGameFactory _gameFactory;
        private readonly LevelModel _levelModel;

        public SZombieSpawner(IGameFactory gameFactory, LevelModel levelModel)
        {
            _gameFactory = gameFactory;
            _levelModel = levelModel;
        }
        
        protected override void OnEnableComponent(CZombieSpawner component)
        {
            base.OnEnableComponent(component);

            CreateZombie(component).Forget();
        }

        private async UniTaskVoid CreateZombie(CZombieSpawner component)
        {
            CZombie zombie = await _gameFactory.CreateZombie(component.ZombieType, component.Position, component.transform.parent);

            CreateStateMachine(zombie);
        }

        private void CreateStateMachine(CZombie zombie)
        {
            zombie.StateMachine.SetStateMachine(new ZombieStateMachine(zombie, _levelModel));

            zombie.StateMachine.UpdateStateMachine
                .Subscribe(_ => zombie.StateMachine.StateMachine.Tick())
                .AddTo(zombie.LifetimeDisposable);
        }
    }
}