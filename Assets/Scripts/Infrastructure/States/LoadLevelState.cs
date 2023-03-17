using CodeBase.Infrastructure.Factories.Game;
using CodeBase.UI;
using CodeBase.UI.Factories;
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

        public LoadLevelState(GameStateMachine stateMachine, 
            SceneLoader sceneLoader, LoadingCurtain curtain, 
            IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
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
    }
}