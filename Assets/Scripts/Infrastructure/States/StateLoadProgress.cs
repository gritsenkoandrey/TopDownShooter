using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateService _stateService;
        private IProgressService _progressService;
        private ILoadingCurtainService _loadingCurtainService;

        public StateLoadProgress(IGameStateService stateService)
        {
            _stateService = stateService;
        }
        
        [Inject]
        private void Construct(IProgressService progressService, ILoadingCurtainService loadingCurtainService)
        {
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