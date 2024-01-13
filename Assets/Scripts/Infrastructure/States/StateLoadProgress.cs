using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace CodeBase.Infrastructure.States
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IProgressService _progressService;
        private readonly ILoadingCurtainService _loadingCurtainService;

        public StateLoadProgress(IGameStateService stateService, IProgressService progressService, ILoadingCurtainService loadingCurtainService)
        {
            _stateService = stateService;
            _progressService = progressService;
            _loadingCurtainService = loadingCurtainService;
        }
        
        void IInitializable.Initialize() => _stateService.AddState(this);

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