using CodeBase.App;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticDataService;
        private readonly IJoystickService _joystickService;
        private readonly IObjectPoolService _objectPoolService;

        public StateBootstrap(IGameStateService stateService, ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticDataService, IJoystickService joystickService, IObjectPoolService objectPoolService)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
            _objectPoolService = objectPoolService;
        }
        
        void IEnterState.Enter()
        {
            LoadResources();
            InitJoystick();
            InitObjectPool();
            
            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        void IExitState.Exit() { }

        private void Next() => _stateService.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
        private void InitJoystick() => _joystickService.Init();
        private void InitObjectPool() => _objectPoolService.Init();
    }
}