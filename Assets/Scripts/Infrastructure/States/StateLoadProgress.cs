using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private ILoadingCurtainService _loadingCurtainService;

        public StateLoadProgress(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        [Inject]
        private void Construct(ILoadingCurtainService loadingCurtainService)
        {
            _loadingCurtainService = loadingCurtainService;
        }

        void IEnterState.Enter()
        {
            _gameStateMachine.Enter<StatePreview, string>(SceneName.Preview);
        }

        void IExitState.Exit()
        {
            _loadingCurtainService.Hide().Forget();
        }
    }
}