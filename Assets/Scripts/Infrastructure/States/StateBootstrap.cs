using CodeBase.App;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace CodeBase.Infrastructure.States
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticDataService;
        private readonly IJoystickService _joystickService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly IAssetService _assetService;

        public StateBootstrap(
            IGameStateService stateService, 
            ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticDataService, 
            IJoystickService joystickService, 
            IObjectPoolService objectPoolService, 
            IAssetService assetService)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
        }

        void IInitializable.Initialize() => _stateService.AddState(this);

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