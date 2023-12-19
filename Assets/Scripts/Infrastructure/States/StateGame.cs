using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Input;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateGame : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IJoystickService _joystickService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        public StateGame(
            IGameStateService stateService, 
            IJoystickService joystickService, 
            IUIFactory uiFactory,
            IGameFactory gameFactory)
        {
            _stateService = stateService;
            _joystickService = joystickService;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Game);
            _joystickService.Enable(true);
            
            SetIdleState();
        }

        void IExitState.Exit()
        {
            _joystickService.Enable(false);
        }

        private void SetIdleState()
        {
            _gameFactory.Character.StateMachine.StateMachine.Enter<CharacterStateIdle>();

            foreach (IEnemy enemy in _gameFactory.Enemies)
            {
                enemy.StateMachine.StateMachine.Enter<ZombieStateIdle>();
            }
        }
    }
}