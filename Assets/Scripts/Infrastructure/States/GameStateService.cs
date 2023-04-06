using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Curtain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loader;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.StaticData;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class GameStateService : IGameStateService
    {
        private Dictionary<Type, IExitState> _states;

        private readonly IObjectResolver _container;
        
        private IExitState _activeState;   
        
        public GameStateService(IObjectResolver container)
        {
            _container = container;
        }

        void IGameStateService.Register()
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(StateBootstrap)] = new StateBootstrap(this, 
                    _container.Resolve<ISceneLoaderService>(), 
                    _container.Resolve<IStaticDataService>()),
                [typeof(StateLoadProgress)] = new StateLoadProgress(this, 
                    _container.Resolve<IProgressService>(), 
                    _container.Resolve<ISaveLoadService>()),
                [typeof(StateLoadLevel)] = new StateLoadLevel(this, 
                    _container.Resolve<ISceneLoaderService>(), 
                    _container.Resolve<IGameFactory>(), 
                    _container.Resolve<IUIFactory>(), 
                    _container.Resolve<IProgressService>(), 
                    _container.Resolve<IAssetService>(), 
                    _container.Resolve<ILoadingCurtainService>(),
                    _container.Resolve<ICameraService>()),
                [typeof(StateGameLoop)] = new StateGameLoop(this)
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