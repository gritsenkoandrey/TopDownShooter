using CodeBase.App;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IStaticDataService _staticDataService;
        private readonly IJoystickService _joystickService;

        public StateBootstrap(IGameStateService stateService, ISceneLoaderService sceneLoaderService, 
            IStaticDataService staticDataService, IJoystickService joystickService)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _staticDataService = staticDataService;
            _joystickService = joystickService;
        }
        
        void IEnterState.Enter()
        {
            LoadResources();
            InitJoystick();
            
            _sceneLoaderService.Load(SceneName.Bootstrap, Next);
        }

        void IExitState.Exit() { }

        private void Next() => _stateService.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
        private void InitJoystick() => _joystickService.Init();
    }
}