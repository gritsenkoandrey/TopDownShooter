namespace CodeBase.Infrastructure.States
{
    public sealed class StateLobby : IEnterState
    {
        private readonly IGameStateService _stateService;

        public StateLobby(IGameStateService stateService)
        {
            _stateService = stateService;
        }

        void IEnterState.Enter() { }
        void IExitState.Exit() { }
    }
}