using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IProgressService _progressService;
        private readonly ILoadingCurtainService _loadingCurtainService;

        public StateLoadProgress(
            IGameStateService stateService, 
            IProgressService progressService, 
            ILoadingCurtainService loadingCurtainService)
        {
            _stateService = stateService;
            _progressService = progressService;
            _loadingCurtainService = loadingCurtainService;
        }
        
        void IEnterState.Enter()
        {
            _progressService.Load();
            _stateService.Enter<StatePreview, string>(SceneName.Lobby);
        }

        void IExitState.Exit()
        {
            _loadingCurtainService.Hide().Forget();
        }
    }
}