using System;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using CodeBase.UI.Screens;
using CodeBase.Utils;
using JetBrains.Annotations;
using UniRx;
using VContainer.Unity;

namespace CodeBase.Infrastructure.States
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateGame : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IJoystickService _joystickService;
        private readonly IUIFactory _uiFactory;
        private readonly LevelModel _levelModel;

        private readonly CompositeDisposable _transitionDisposable = new();

        public StateGame(IGameStateService stateService, IJoystickService joystickService, IUIFactory uiFactory, LevelModel levelModel)
        {
            _stateService = stateService;
            _joystickService = joystickService;
            _uiFactory = uiFactory;
            _levelModel = levelModel;
        }
        
        void IInitializable.Initialize() => _stateService.AddState(this);

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Game);
            _joystickService.Enable(true);
            
            ActivateUnitStateMachine();
            
            SubscribeOnWin();
            SubscribeOnLose();
            SubscribeOnTimeLeft();
        }

        void IExitState.Exit()
        {
            _joystickService.Enable(false);
            _transitionDisposable.Clear();
        }

        private void ActivateUnitStateMachine()
        {
            _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateIdle>();
            _levelModel.Enemies.Foreach(enemy => enemy.StateMachine.StateMachine.Enter<ZombieStateIdle>());
        }

        private void SubscribeOnWin()
        {
            _levelModel.Enemies
                .ObserveRemove()
                .Where(_ => AllEnemyIsDeath())
                .First()
                .Subscribe(_ => Win())
                .AddTo(_transitionDisposable);

        }

        private void SubscribeOnLose()
        {
            _levelModel.Character.Health.CurrentHealth
                .Where(_ => CharacterIsDeath())
                .First()
                .Subscribe(_ => Lose())
                .AddTo(_transitionDisposable);
        }

        private void SubscribeOnTimeLeft()
        {
            Observable.Timer(TimeLeft())
                .First()
                .Delay(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => Lose())
                .AddTo(_transitionDisposable);
        }

        private void Win()
        {
            _stateService.Enter<StateWin>();
            _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateNone>();
        }

        private void Lose()
        {
            _stateService.Enter<StateFail>();
            _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateDeath>();
            _levelModel.Enemies.Foreach(SetEnemyStateNone);
        }

        private bool AllEnemyIsDeath() => _levelModel.Enemies.Count == 0;
        private bool CharacterIsDeath() => _levelModel.Character.Health.IsAlive == false;
        private void SetEnemyStateNone(IEnemy enemy) => enemy.StateMachine.StateMachine.Enter<ZombieStateNone>();
        private TimeSpan TimeLeft() => TimeSpan.FromSeconds(_levelModel.Level.LevelTime);
    }
}