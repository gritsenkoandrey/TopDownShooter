namespace CodeBase.Infrastructure.States
{
    public sealed class StateGameLoop : IEnterState
    {
        private readonly IGameStateService _stateService;

        public StateGameLoop(IGameStateService stateService)
        {
            _stateService = stateService;
        }

        void IEnterState.Enter() { }
        void IExitState.Exit() { }
    }
}