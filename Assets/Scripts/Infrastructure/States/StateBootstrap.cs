using CodeBase.App;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
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
        private readonly IGameStateMachine _gameStateMachine;
        private ISceneLoaderService _sceneLoaderService;
        private IStaticDataService _staticDataService;
        private IJoystickService _joystickService;
        private IObjectPoolService _objectPoolService;
        private IAssetService _assetService;
        private ICameraService _cameraService;

        public StateBootstrap(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(
            ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticDataService, 
            IJoystickService joystickService, 
            IObjectPoolService objectPoolService, 
            IAssetService assetService,
            ICameraService cameraService)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
            _cameraService = cameraService;
        }

        async void IEnterState.Enter()
        {
            LoadResources();
            await InitAsset();
            await InitObjectPool();
            InitJoystick();
            InitCameraService();

            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        void IExitState.Exit() { }

        private void Next() => _gameStateMachine.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
        private async UniTask InitAsset() => await _assetService.Init();
        private async UniTask InitObjectPool() => await _objectPoolService.Init();
        private void InitJoystick() => _joystickService.Init();
        private void InitCameraService() => _cameraService.Init();
    }
}