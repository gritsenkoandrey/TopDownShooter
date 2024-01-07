using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateWin : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progressService;

        public StateWin(
            IGameStateService stateService, 
            IUIFactory uiFactory, 
            IProgressService progressService)
        {
            _stateService = stateService;
            _uiFactory = uiFactory;
            _progressService = progressService;
        }

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Win);
            _progressService.LevelData.Data.Value++;
        }

        void IExitState.Exit() { }
    }
}