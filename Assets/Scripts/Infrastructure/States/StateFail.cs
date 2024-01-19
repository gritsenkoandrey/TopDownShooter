using System;
using CodeBase.App;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateFail : IEnterState
    {
        private readonly IGameStateService _stateService;
        private IUIFactory _uiFactory;

        private IDisposable _transitionDisposable;

        public StateFail(IGameStateService stateService)
        {
            _stateService = stateService;
        }

        [Inject]
        public void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        void IEnterState.Enter()
        {
            SubscribeOnTransition().Forget();
        }

        void IExitState.Exit()
        {
            _transitionDisposable?.Dispose();
        }

        private async UniTaskVoid SubscribeOnTransition()
        {
            BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Lose);

            _transitionDisposable = screen.ChangeState.First().Subscribe(ChangeState);
        }

        private void ChangeState(Unit _) => _stateService.Enter<StatePreview, string>(SceneName.Lobby);
    }
}