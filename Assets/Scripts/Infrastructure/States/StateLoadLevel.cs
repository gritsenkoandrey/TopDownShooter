using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Progress;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadLevel : IEnterLoadState<string>
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progressService;
        private readonly IAssetService _assetService;
        private readonly ILoadingCurtainService _curtain;
        private readonly ICameraService _cameraService;

        public StateLoadLevel(IGameStateService stateService, ISceneLoaderService sceneLoaderService, 
            IGameFactory gameFactory, IUIFactory uiFactory, IProgressService progressService, 
            IAssetService assetService, ILoadingCurtainService curtain, ICameraService cameraService)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _progressService = progressService;
            _assetService = assetService;
            _curtain = curtain;
            _cameraService = cameraService;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            CleanUpWorld();

            _curtain.Show();
            _sceneLoaderService.Load(sceneName, Next);
        }

        async void IExitState.Exit()
        {
            await _curtain.Hide();
        }

        private void Next()
        {
            CreateWorld();
            ReadProgress();

            _stateService.Enter<StateGameLoop>();
        }

        private void CleanUpWorld()
        {
            _uiFactory.CleanUp();
            _gameFactory.CleanUp();
            _assetService.Unload();
            _cameraService.CleanUp();
        }

        private void CreateWorld()
        {
            _uiFactory.CreateCanvas();
            _uiFactory.CreateScreen(ScreenType.Lobby);
            _gameFactory.CreateCharacter();
            _gameFactory.CreateLevel();
        }

        private void ReadProgress()
        {
            foreach (IProgressReader progress in _uiFactory.ProgressReaders)
            {
                progress.Read(_progressService.PlayerProgress);
            }

            foreach (IProgressReader progress in _gameFactory.ProgressReaders)
            {
                progress.Read(_progressService.PlayerProgress);
            }
        }
    }
}