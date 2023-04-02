using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticDataService;
        
        private const string Initial = "Initial";

        public StateBootstrap(IGameStateService stateService, ISceneLoaderService sceneLoaderService, IStaticDataService staticDataService)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
        }
        
        void IEnterState.Enter()
        {
            LoadResources();
            
            _sceneLoaderService.Load(Initial, Next);
        }

        void IExitState.Exit() { }

        private void Next() => _stateService.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
    }
}