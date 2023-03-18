using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UI.Factories;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public sealed class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        
        private const string Initial = "Initial";

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterService();
            LoadResources();
        }
        
        public void Enter()
        {
            Debug.Log("Enter BootstrapState");

            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            Debug.Log("Exit BootstrapState");
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterService()
        {
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAsset>(new AssetProvider());
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAsset>(), _services.Single<IStaticDataService>()));
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAsset>(), _stateMachine, _services.Single<IStaticDataService>()));
        }

        private void LoadResources()
        {
            _services.Single<IStaticDataService>().Load();
        }
    }
}