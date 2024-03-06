using System;
using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateWin : IEnterState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;
        private IProgressService _progressService;
        private ILoadingCurtainService _loadingCurtainService;
        
        private IDisposable _transitionDisposable;

        public StateWin(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        [Inject]
        private void Construct(IUIFactory uiFactory, IProgressService progressService, ILoadingCurtainService loadingCurtainService)
        {
            _uiFactory = uiFactory;
            _progressService = progressService;
            _loadingCurtainService = loadingCurtainService;
        }

        void IEnterState.Enter()
        {
            _progressService.LevelData.Data.Value++;
            
            SubscribeOnTransition().Forget();
        }

        void IExitState.Exit()
        {
            _transitionDisposable?.Dispose();
            
            _loadingCurtainService.Show();
        }
        
        private async UniTaskVoid SubscribeOnTransition()
        {
            BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Win);

            _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
        }

        private void ChangeState(Unit _) => _gameStateMachine.Enter<StatePreview, string>(SceneName.Preview);
    }
}