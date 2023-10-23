using CodeBase.App;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticDataService;
        private readonly IJoystickService _joystickService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly IAssetService _assetService;

        public StateBootstrap(IGameStateService stateService, ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticDataService, IJoystickService joystickService, IObjectPoolService objectPoolService,
            IAssetService assetService)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
        }
        
        async void IEnterState.Enter()
        {
            await InitAsset();
            await LoadResources();
            InitJoystick();
            InitObjectPool();
            
            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        void IExitState.Exit() { }

        private void Next() => _stateService.Enter<StateLoadProgress>();
        private async UniTask LoadResources() => await _staticDataService.Load();
        private async UniTask InitAsset() => await _assetService.Init();
        private void InitJoystick() => _joystickService.Init();
        private void InitObjectPool() => _objectPoolService.Init();
    }
}