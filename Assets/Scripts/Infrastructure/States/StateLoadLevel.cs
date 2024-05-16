using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.GUI;
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
        private IAssetService _assetService;
        private ILoadingCurtainService _loadingCurtain;
        private ICameraService _cameraService;
        private ITextureArrayFactory _textureArrayFactory;
        private IGuiService _guiService;
        private LevelModel _levelModel;

        public StateLoadLevel(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        [Inject]
        private void Construct(
            ISceneLoaderService sceneLoaderService, 
            IGameFactory gameFactory, 
            IAssetService assetService, 
            ILoadingCurtainService loadingCurtain, 
            ICameraService cameraService,
            ITextureArrayFactory textureArrayFactory,
            IGuiService guiService,
            LevelModel levelModel)
        {
            _sceneLoaderService = sceneLoaderService;
            _gameFactory = gameFactory;
            _assetService = assetService;
            _loadingCurtain = loadingCurtain;
            _cameraService = cameraService;
            _textureArrayFactory = textureArrayFactory;
            _guiService = guiService;
            _levelModel = levelModel;
        }

        void IEnterLoadState<string>.Enter(string sceneName)
        {
            CleanUpWorld();

            _loadingCurtain.Show();
            
            _sceneLoaderService.Load(sceneName, Next);
        }

        void IExitState.Exit()
        {
            _loadingCurtain.Hide();
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
            _cameraService.CleanUp();
            _textureArrayFactory.CleanUp();
            _assetService.CleanUp();
            _levelModel.CleanUp();
            _guiService.CleanUp();
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