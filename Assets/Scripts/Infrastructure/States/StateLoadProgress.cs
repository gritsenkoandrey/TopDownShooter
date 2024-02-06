using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private IProgressService _progressService;
        private ILoadingCurtainService _loadingCurtainService;

        public StateLoadProgress(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
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
            _gameStateMachine.Enter<StatePreview, string>(SceneName.Preview);
        }

        void IExitState.Exit()
        {
            _loadingCurtainService.Hide().Forget();
        }
    }
}