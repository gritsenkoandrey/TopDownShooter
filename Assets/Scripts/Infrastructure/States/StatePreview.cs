using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.States
{
    public class StatePreview : IEnterLoadState<string>
    {
        private readonly IGameStateService _stateService;
        private readonly IUIFactory _uiFactory;
        private readonly ISceneLoaderService _sceneLoaderService;

        public StatePreview(
            IGameStateService stateService,
            IUIFactory uiFactory,
            ISceneLoaderService sceneLoaderService)
        {
            _stateService = stateService;
            _uiFactory = uiFactory;
            _sceneLoaderService = sceneLoaderService;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            _sceneLoaderService.Load(sceneName, Next);
        }

        void IExitState.Exit()
        {
        }

        private void Next()
        {
            _uiFactory.CreateScreen(ScreenType.Preview);
        }
    }
}