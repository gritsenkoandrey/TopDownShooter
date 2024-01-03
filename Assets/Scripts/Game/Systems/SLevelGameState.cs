using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SLevelGameState : SystemComponent<CCharacter>
    {
        private readonly IGameStateService _gameStateService;
        private readonly LevelModel _levelModel;

        public SLevelGameState(IGameStateService gameStateService, LevelModel levelModel)
        {
            _gameStateService = gameStateService;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CCharacter character)
        {
            base.OnEnableComponent(character);
            
            SubscribeOnWinGame(character);
            SubscribeOnFailGame(character);
        }

        private void SubscribeOnWinGame(CCharacter character)
        {
            _levelModel.Enemies
                .ObserveRemove()
                .Where(_ => AllEnemyIsDeath())
                .First()
                .Subscribe(_ => Win(character))
                .AddTo(character.LifetimeDisposable);
        }

        private bool AllEnemyIsDeath() => _levelModel.Enemies.Count == 0;

        private void Win(CCharacter character)
        {
            _gameStateService.Enter<StateWin>();
            
            character.StateMachine.StateMachine.Enter<CharacterStateNone>();
        }

        private void SubscribeOnFailGame(CCharacter character)
        {
            character.Health.CurrentHealth
                .SkipLatestValueOnSubscribe()
                .Where(_ => CharacterIsDeath(character))
                .First()
                .Subscribe(_ => Lose(character))
                .AddTo(character.LifetimeDisposable);
        }

        private bool CharacterIsDeath(CCharacter character) => !character.Health.IsAlive;

        private void Lose(CCharacter character)
        {
            _gameStateService.Enter<StateFail>();
            
            character.StateMachine.StateMachine.Enter<CharacterStateDeath>();
            
            _levelModel.Enemies.Foreach(SetEnemyStateNone);
        }

        private void SetEnemyStateNone(IEnemy enemy) => enemy.StateMachine.StateMachine.Enter<ZombieStateNone>();
    }
}