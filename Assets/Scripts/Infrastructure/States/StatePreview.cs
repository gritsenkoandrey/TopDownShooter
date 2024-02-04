using System;
using CodeBase.App;
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
        private readonly IGameStateService _stateService;
        private IUIFactory _uiFactory;
        private ISceneLoaderService _sceneLoaderService;
        
        private IDisposable _transitionDisposable;

        public StatePreview(IGameStateService stateService)
        {
            _stateService = stateService;
        }

        [Inject]
        private void Construct(IUIFactory uiFactory, ISceneLoaderService sceneLoaderService)
        {
            _uiFactory = uiFactory;
            _sceneLoaderService = sceneLoaderService;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            _sceneLoaderService.Load(sceneName, Next);
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

            _transitionDisposable = screen.ChangeState.First().Subscribe(ChangeState);
        }

        private void ChangeState(Unit _) => _stateService.Enter<StateLoadLevel, string>(SceneName.Game);
    }
}