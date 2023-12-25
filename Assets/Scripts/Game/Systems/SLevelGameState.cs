using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.States;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SLevelGameState : SystemComponent<CCharacter>
    {
        private readonly IGameStateService _gameStateService;
        private readonly IGameFactory _gameFactory;
        private readonly LevelModel _levelModel;

        public SLevelGameState(IGameStateService gameStateService, IGameFactory gameFactory, LevelModel levelModel)
        {
            _gameStateService = gameStateService;
            _gameFactory = gameFactory;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);
            
            SubscribeOnWinGame(component);
            SubscribeOnFailGame(component);
        }

        private void SubscribeOnWinGame(CCharacter component)
        {
            _levelModel.Enemies
                .ObserveRemove()
                .Subscribe(_ => Win(component))
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnFailGame(CCharacter component)
        {
            component.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ => Lose(component))
                .AddTo(component.LifetimeDisposable);
        }

        private void Win(CCharacter component)
        {
            if (_levelModel.Enemies.Count == 0)
            {
                component.StateMachine.StateMachine.Enter<CharacterStateNone>();
                _gameStateService.Enter<StateWin>();
            }
        }

        private void Lose(CCharacter component)
        {
            if (!component.Health.IsAlive)
            {
                component.StateMachine.StateMachine.Enter<CharacterStateDeath>();
                _gameStateService.Enter<StateFail>();

                foreach (IEnemy enemy in _levelModel.Enemies)
                {
                    enemy.StateMachine.StateMachine.Enter<ZombieStateNone>();
                }
            }
        }
    }
}