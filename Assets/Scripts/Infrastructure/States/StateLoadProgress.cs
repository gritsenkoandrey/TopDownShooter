using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IObjectResolver _container;
        
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        private const string Main = "Main";

        public StateLoadProgress(IGameStateMachine stateMachine, IObjectResolver container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }
        
        public void Enter()
        {
            _progressService = _container.Resolve<IProgressService>();
            _saveLoadService = _container.Resolve<ISaveLoadService>();

            LoadProgress();
            
            _stateMachine.Enter<StateLoadLevel, string>(Main);
        }

        public void Exit() { }

        private void LoadProgress()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
        }
    }
}