using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Models;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadLevel : IEnterLoadState<string>
    {
        private readonly IGameStateService _stateService;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IAssetService _assetService;
        private readonly ILoadingCurtainService _curtain;
        private readonly ICameraService _cameraService;
        private readonly ITextureArrayFactory _textureArrayFactory;
        private readonly LevelModel _levelModel;

        public StateLoadLevel(
            IGameStateService stateService, 
            ISceneLoaderService sceneLoaderService, 
            IGameFactory gameFactory, 
            IUIFactory uiFactory, 
            IAssetService assetService, 
            ILoadingCurtainService curtain, 
            ICameraService cameraService,
            ITextureArrayFactory textureArrayFactory,
            LevelModel levelModel)
        {
            _stateService = stateService;
            _sceneLoaderService = sceneLoaderService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _assetService = assetService;
            _curtain = curtain;
            _cameraService = cameraService;
            _textureArrayFactory = textureArrayFactory;
            _levelModel = levelModel;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            CleanUpWorld();

            _curtain.Show();
            
            _sceneLoaderService.Load(sceneName, Next);
        }

        void IExitState.Exit()
        {
            _curtain.Hide().Forget();
        }

        private void Next()
        {
            CreateTextureArray();
            CreateScreen();
            CreateLevel().Forget();
        }

        private void CleanUpWorld()
        {
            _uiFactory.CleanUp();
            _gameFactory.CleanUp();
            _cameraService.CleanUp();
            _textureArrayFactory.CleanUp();
            _assetService.CleanUp();
            _levelModel.CleanUp();
        }

        private async UniTaskVoid CreateLevel()
        {
            await _gameFactory.CreateLevel();
            
            _stateService.Enter<StateLobby>();
        }

        private void CreateScreen()
        {
            _uiFactory.CreateScreen(ScreenType.Lobby);
        }

        private void CreateTextureArray()
        {
            _textureArrayFactory.CreateTextureArray();
            _textureArrayFactory.GenerateRandomTextureIndex();
        }
    }
}