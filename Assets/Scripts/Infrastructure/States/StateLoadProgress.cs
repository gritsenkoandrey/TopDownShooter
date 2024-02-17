using CodeBase.App;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadProgress : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public StateLoadProgress(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        void IEnterState.Enter()
        {
            _gameStateMachine.Enter<StatePreview, string>(SceneName.Preview);
        }

        void IExitState.Exit()
        {
        }
    }
}