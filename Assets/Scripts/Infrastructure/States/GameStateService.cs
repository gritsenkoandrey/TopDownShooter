using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public sealed class GameStateService : IGameStateService
    {
        private readonly Dictionary<Type, IExitState> _states;

        private IExitState _activeState;
        
        public GameStateService(ISceneLoaderService sceneLoaderService, IStaticDataService staticDataService, 
            IProgressService progressService, ISaveLoadService saveLoadService, IGameFactory gameFactory, 
            IUIFactory uiFactory, IAssetService assetService, ILoadingCurtainService loadingCurtainService, 
            ICameraService cameraService, IJoystickService joystickService, ITextureArrayFactory textureArrayFactory)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(StateBootstrap)] = new StateBootstrap(this, 
                    sceneLoaderService, 
                    staticDataService),
                [typeof(StateLoadProgress)] = new StateLoadProgress(this, 
                    progressService, 
                    saveLoadService),
                [typeof(StateLoadLevel)] = new StateLoadLevel(this, 
                    sceneLoaderService, 
                    gameFactory, 
                    uiFactory, 
                    progressService, 
                    assetService, 
                    loadingCurtainService,
                    cameraService,
                    textureArrayFactory),
                [typeof(StateLobby)] = new StateLobby(this),
                [typeof(StateGame)] = new StateGame(this, 
                    joystickService, 
                    uiFactory),
                [typeof(StateFail)] = new StateFail(this, 
                    uiFactory),
                [typeof(StateWin)] = new StateWin(this, 
                    uiFactory,
                    progressService,
                    saveLoadService)
            };
        }
        
        void IGameStateService.Enter<TState>()
        {
            TState state = ChangeState<TState>();

            state.Enter();
        }

        void IGameStateService.Enter<TState, TLoad>(TLoad load)
        {
            TState state = ChangeState<TState>();
            
            state.Enter(load);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();

            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}