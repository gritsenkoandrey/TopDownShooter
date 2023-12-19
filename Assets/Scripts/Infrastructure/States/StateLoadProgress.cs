using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILoadingCurtainService _loadingCurtainService;

        public StateLoadProgress(
            IGameStateService stateService, 
            IProgressService progressService, 
            ISaveLoadService saveLoadService,
            ILoadingCurtainService loadingCurtainService)
        {
            _stateService = stateService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _loadingCurtainService = loadingCurtainService;
        }
        
        void IEnterState.Enter()
        {
            LoadProgress();
            _stateService.Enter<StatePreview, string>(SceneName.Lobby);
        }

        void IExitState.Exit()
        {
            _loadingCurtainService.Hide().Forget();
        }

        private void LoadProgress()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
        }
    }
}