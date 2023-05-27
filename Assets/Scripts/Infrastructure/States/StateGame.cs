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

        public StateGame(IGameStateService stateService, IJoystickService joystickService, IUIFactory uiFactory)
        {
            _stateService = stateService;
            _joystickService = joystickService;
            _uiFactory = uiFactory;
        }

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Game);
            _joystickService.Enable(true);
        }

        void IExitState.Exit()
        {
            _joystickService.Enable(false);
        }
    }
}