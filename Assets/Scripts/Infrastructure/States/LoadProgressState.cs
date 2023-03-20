using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public sealed class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private const string Main = "Main";

        public LoadProgressState(GameStateMachine stateMachine, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter()
        {
            Debug.Log("Enter LoadProgressState");
            
            LoadProgress();
            
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadProgressState");
        }

        private void LoadProgress()
        {
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
        }
    }
}