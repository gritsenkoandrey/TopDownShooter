using CodeBase.Infrastructure.Factories.UI;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateFail : IEnterState
    {
        private readonly IGameStateService _stateService;
        private readonly IUIFactory _uiFactory;

        public StateFail(IGameStateService stateService, IUIFactory uiFactory)
        {
            _stateService = stateService;
            _uiFactory = uiFactory;
        }

        void IEnterState.Enter()
        {
            _uiFactory.CreateScreen(ScreenType.Lose);
        }

        void IExitState.Exit() { }
    }
}