using System;
using System.Collections.Generic;

namespace CodeBase.Game.StateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        private IState _activeState;

        protected IDictionary<Type, IState> States;
        
        void IStateMachine.Enter<T>()
        {
            T state = ChangeState<T>();

            state.Enter();
        }

        void IStateMachine.Tick() => _activeState?.Tick();

        private T ChangeState<T>() where T : class, IState
        {
            _activeState?.Exit();

            T state = GetState<T>();

            _activeState = state;
            
            return state;
        }

        private T GetState<T>() where T : class, IState
        {
            return States[typeof(T)] as T;
        }
    }
}