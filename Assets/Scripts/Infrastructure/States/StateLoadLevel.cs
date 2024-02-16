using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadLevel : IEnterLoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private ISceneLoaderService _sceneLoaderService;
        private IGameFactory _gameFactory;
        private IUIFactory _uiFactory;
        private IAssetService _assetService;
        private ILoadingCurtainService _curtain;
        private ICameraService _cameraService;
        private ITextureArrayFactory _textureArrayFactory;
        private LevelModel _levelModel;

        public StateLoadLevel(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(
            ISceneLoaderService sceneLoaderService, 
            IGameFactory gameFactory, 
            IUIFactory uiFactory, 
            IAssetService assetService, 
            ILoadingCurtainService curtain, 
            ICameraService cameraService,
            ITextureArrayFactory textureArrayFactory,
            LevelModel levelModel)
        {
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
            PrepareLevel().Forget();
        }

        private async UniTaskVoid PrepareLevel()
        {
            await CreateTextureArray();
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
            _gameStateMachine.Enter<StateLobby>();
        }

        private async UniTask CreateTextureArray()
        {
            await _textureArrayFactory.CreateTextureArray();
            _textureArrayFactory.GenerateRandomTextureIndex();
        }
    }
}