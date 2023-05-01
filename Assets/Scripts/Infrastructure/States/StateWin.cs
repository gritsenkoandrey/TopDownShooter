using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateWin : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public StateWin(IGameStateService stateService, IUIFactory uiFactory, 
            IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateService = stateService;
            _uiFactory = uiFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Win);
            _progressService.PlayerProgress.Level++;
            _saveLoadService.SaveProgress();
        }

        void IExitState.Exit() { }
    }
}