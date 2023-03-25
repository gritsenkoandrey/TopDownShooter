using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        
        private IExitState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.Single<IProgressService>(), 
                    services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
                    services.Single<IGameFactory>(), 
                    services.Single<IUIFactory>(),
                    services.Single<IProgressService>(),
                    services.Single<IAsset>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();

            state.Enter();
        }

        public void Enter<TState, TLoad>(TLoad load) where TState : class, ILoadState<TLoad>
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