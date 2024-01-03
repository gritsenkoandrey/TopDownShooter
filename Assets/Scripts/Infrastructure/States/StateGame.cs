using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using CodeBase.UI.Screens;
using CodeBase.Utils;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateGame : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IJoystickService _joystickService;
        private readonly IUIFactory _uiFactory;
        private readonly LevelModel _levelModel;

        public StateGame(
            IGameStateService stateService, 
            IJoystickService joystickService, 
            IUIFactory uiFactory,
            LevelModel levelModel)
        {
            _stateService = stateService;
            _joystickService = joystickService;
            _uiFactory = uiFactory;
            _levelModel = levelModel;
        }

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Game);
            _joystickService.Enable(true);
            
            StartUnitStateMachine();
        }

        void IExitState.Exit()
        {
            _joystickService.Enable(false);
        }

        private void StartUnitStateMachine()
        {
            _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateIdle>();
            _levelModel.Enemies.Foreach(enemy => enemy.StateMachine.StateMachine.Enter<ZombieStateIdle>());
        }
    }
}