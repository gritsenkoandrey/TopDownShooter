using System;
using CodeBase.App;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public class StatePreview : IEnterLoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;
        private ISceneLoaderService _sceneLoaderService;
        private ILoadingCurtainService _loadingCurtainService;
        
        private IDisposable _transitionDisposable;

        public StatePreview(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(IUIFactory uiFactory, ISceneLoaderService sceneLoaderService, ILoadingCurtainService loadingCurtainService)
        {
            _uiFactory = uiFactory;
            _sceneLoaderService = sceneLoaderService;
            _loadingCurtainService = loadingCurtainService;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            _sceneLoaderService.Load(sceneName, Next);
            _loadingCurtainService.Hide().Forget();
        }

        void IExitState.Exit()
        {
            _transitionDisposable?.Dispose();
        }

        private void Next()
        {
            SubscribeOnTransition().Forget();
        }
        
        private async UniTaskVoid SubscribeOnTransition()
        {
            BaseScreen screen = await _uiFactory.CreateScreen(ScreenType.Preview);

            _transitionDisposable = screen.CloseScreen.First().Subscribe(ChangeState);
        }

        private void ChangeState(Unit _) => _gameStateMachine.Enter<StateLoadLevel, string>(SceneName.Game);
    }
}