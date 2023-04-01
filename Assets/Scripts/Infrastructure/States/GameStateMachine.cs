using System;
using System.Collections.Generic;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        
        private IExitState _activeState;   
        
        public GameStateMachine(IObjectResolver container)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(StateBootstrap)] = new StateBootstrap(this, container),
                [typeof(StateLoadProgress)] = new StateLoadProgress(this, container),
                [typeof(StateLoadLevel)] = new StateLoadLevel(this, container),
                [typeof(StateGameLoop)] = new StateGameLoop(this)
            };
        }
        
        public void Enter<TState>() where TState : class, IEnterState
        {
            TState state = ChangeState<TState>();

            state.Enter();
        }

        public void Enter<TState, TLoad>(TLoad load) where TState : class, IEnterLoadState<TLoad>
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