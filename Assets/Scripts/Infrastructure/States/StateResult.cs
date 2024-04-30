using System;
using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateResult : IEnterLoadState<bool>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;
        private ILoadingCurtainService _loadingCurtainService;

        private IDisposable _transitionDisposable;

        public StateResult(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IUIFactory uiFactory, ILoadingCurtainService loadingCurtainService)
        {
            _uiFactory = uiFactory;
            _loadingCurtainService = loadingCurtainService;
        }

        void IEnterLoadState<bool>.Enter(bool isWin)
        {
            SubscribeOnTransition(isWin).Forget();
        }

        void IExitState.Exit()
        {
            _transitionDisposable?.Dispose();
            _loadingCurtainService.Show();
        }

        private async UniTaskVoid SubscribeOnTransition(bool isWin)
        {
            BaseScreen screen;

            if (isWin)
            {
                screen = await _uiFactory.CreateScreen(ScreenType.Win);
            }
            else
            {
                screen = await _uiFactory.CreateScreen(ScreenType.Lose);
            }

            _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
        }

        private void ChangeState(Unit _) => _gameStateMachine.Enter<StatePreview, string>(SceneName.Preview);
    }
}