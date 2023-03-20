using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public sealed class LoadLevelState : ILoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, 
            SceneLoader sceneLoader, LoadingCurtain curtain, 
            IGameFactory gameFactory, IUIFactory uiFactory, IProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            Debug.Log("Enter LoadLevelState");
            
            CleanUpWorld();

            _curtain.Show();
            
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadLevelState");
            
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            CreateWorld();
            ReadProgress();

            _stateMachine.Enter<GameLoopState>();
        }

        private void CleanUpWorld()
        {
            _uiFactory.CleanUp();
            _gameFactory.CleanUp();
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