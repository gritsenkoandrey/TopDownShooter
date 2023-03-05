using UnityEngine;

namespace AndreyGritsenko.Infrastructure.States
{
    public sealed class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private const string Main = "Main";

        public LoadProgressState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            Debug.Log("Enter LoadProgressState");
            
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadProgressState");
        }
    }
}