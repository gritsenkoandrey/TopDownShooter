using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.StaticData;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateBootstrap : IEnterState
    {
        private readonly IObjectResolver _container;
        private readonly IGameStateMachine _stateMachine;
        
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;
        
        private const string Initial = "Initial";

        public StateBootstrap(IGameStateMachine stateMachine, IObjectResolver container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            _sceneLoader = _container.Resolve<ISceneLoader>();
            _staticDataService = _container.Resolve<IStaticDataService>();

            LoadResources();
            
            _sceneLoader.Load(Initial, Next);
        }

        public void Exit() { }
        private void Next() => _stateMachine.Enter<StateLoadProgress>();
        private void LoadResources() => _staticDataService.Load();
    }
}