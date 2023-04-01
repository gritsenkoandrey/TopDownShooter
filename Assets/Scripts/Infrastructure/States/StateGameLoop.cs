namespace CodeBase.Infrastructure.States
{
    public sealed class StateGameLoop : IEnterState
    {
        private readonly IGameStateMachine _stateMachine;

        public StateGameLoop(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter() { }
        public void Exit() { }
    }
}