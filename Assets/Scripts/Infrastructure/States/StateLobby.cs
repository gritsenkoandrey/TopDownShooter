using System;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLobby : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;

        private IDisposable _transitionDisposable;

        public StateLobby(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IUIFactory uiFactory)
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
            BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Lobby);

            _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
        }

        private void ChangeState(Unit _) => _gameStateMachine.Enter<StateGame>();
    }
}