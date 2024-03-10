using CodeBase.App;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
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
        private IProgressService _progressService;

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
            ICameraService cameraService,
            IProgressService progressService)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
            _cameraService = cameraService;
            _progressService = progressService;
        }

        void IEnterState.Enter()
        {
            Init().Forget();
        }

        void IExitState.Exit() { }

        private async UniTaskVoid Init()
        {
            LoadResources();
            await InitAsset();
            await InitObjectPool();
            InitJoystick();
            InitCameraService();
            InitProgress();

            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        private void Next() => _gameStateMachine.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
        private async UniTask InitAsset() => await _assetService.Init();
        private async UniTask InitObjectPool() => await _objectPoolService.Init();
        private void InitJoystick() => _joystickService.Init();
        private void InitCameraService() => _cameraService.Init();
        private void InitProgress() => _progressService.Init();
    }
}