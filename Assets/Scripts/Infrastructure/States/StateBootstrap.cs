﻿using CodeBase.App;
using CodeBase.Infrastructure.AppSettingsService;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.CheatService;
using CodeBase.Infrastructure.Haptic;
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
        private IHapticService _hapticService;
        private ICheatService _cheatService;
        private IAppSettingsService _appSettingsService;

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
            IProgressService progressService,
            IHapticService hapticService,
            ICheatService cheatService,
            IAppSettingsService appSettingsService)
        {
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
            _cameraService = cameraService;
            _progressService = progressService;
            _hapticService = hapticService;
            _cheatService = cheatService;
            _appSettingsService = appSettingsService;
        }

        void IEnterState.Enter()
        {
            Init().Forget();
        }

        void IExitState.Exit() { }

        private async UniTaskVoid Init()
        {
            InitAppSettings();

            LoadResources();
            await InitAsset();
            await InitObjectPool();
            InitJoystick();
            InitCameraService();
            InitProgress();
            InitHaptic();
            InitCheat();

            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        private void Next() => _gameStateMachine.Enter<StateLoadProgress>();
        private void InitAppSettings()
        {
            _appSettingsService.Init();
            _appSettingsService.SetFrameRate(60);
        }

        private void LoadResources() => _staticDataService.Load();
        private async UniTask InitAsset() => await _assetService.Init();
        private async UniTask InitObjectPool() => await _objectPoolService.Init();
        private void InitJoystick() => _joystickService.Init();
        private void InitCameraService() => _cameraService.Init();
        private void InitProgress() => _progressService.Init();
        private void InitHaptic() => _hapticService.Init();
        private void InitCheat() => _cheatService.Init();
    }
}