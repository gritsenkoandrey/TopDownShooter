using CodeBase.App;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public StateLoadProgress(
            IGameStateService stateService, 
            IProgressService progressService, 
            ISaveLoadService saveLoadService)
        {
            _stateService = stateService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        void IEnterState.Enter()
        {
            LoadProgress();
            
            _stateService.Enter<StateLoadLevel, string>(SceneName.Main);
        }

        void IExitState.Exit() { }

        private void LoadProgress()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
        }
    }
}