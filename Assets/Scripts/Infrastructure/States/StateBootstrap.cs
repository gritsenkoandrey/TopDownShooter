using CodeBase.App;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IGameStateService _stateService;
        private ISceneLoaderService _sceneLoaderService;
        private IStaticDataService _staticDataService;
        private IJoystickService _joystickService;
        private IObjectPoolService _objectPoolService;
        private IAssetService _assetService;

        public StateBootstrap(IGameStateService stateService)
        {
            _stateService = stateService;
        }

        [Inject]
        public void Construct(
            ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticDataService, 
            IJoystickService joystickService, 
            IObjectPoolService objectPoolService, 
            IAssetService assetService)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
        }

        async void IEnterState.Enter()
        {
            LoadResources();
            await InitAsset();
            await InitObjectPool();
            InitJoystick();

            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        void IExitState.Exit() { }

        private void Next() => _stateService.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
        private async UniTask InitAsset() => await _assetService.Init();
        private async UniTask InitObjectPool() => await _objectPoolService.Init();
        private void InitJoystick() => _joystickService.Init();
    }
}