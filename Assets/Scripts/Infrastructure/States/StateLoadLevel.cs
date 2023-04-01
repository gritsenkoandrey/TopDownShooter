using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Progress;
using CodeBase.UI.Screens;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class StateLoadLevel : IEnterLoadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IObjectResolver _container;
        
        private ISceneLoader _sceneLoader;
        private IGameFactory _gameFactory;
        private IUIFactory _uiFactory;
        private IProgressService _progressService;
        private IAsset _asset;
        private LoadingCurtain _curtain;

        public StateLoadLevel(IGameStateMachine stateMachine, IObjectResolver container)
        {
            _stateMachine = stateMachine;
            _container = container;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader = _container.Resolve<ISceneLoader>();
            _curtain = _container.Resolve<LoadingCurtain>();
            _gameFactory = _container.Resolve<IGameFactory>();
            _uiFactory = _container.Resolve<IUIFactory>();
            _progressService = _container.Resolve<IProgressService>();;
            _asset = _container.Resolve<IAsset>();

            CleanUpWorld();

            _curtain.Show();
            
            _sceneLoader.Load(sceneName, Next);
        }

        public void Exit() => _curtain.Hide();

        private void Next()
        {
            CreateWorld();
            ReadProgress();

            _stateMachine.Enter<StateGameLoop>();
        }

        private void CleanUpWorld()
        {
            _uiFactory.CleanUp();
            _gameFactory.CleanUp();
            _asset.Unload();
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