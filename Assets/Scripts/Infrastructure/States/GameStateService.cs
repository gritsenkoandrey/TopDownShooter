using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.States
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class GameStateService : IGameStateService
    {
        private readonly IDictionary<Type, IExitState> _states;

        private IExitState _activeState;

        public GameStateService()
        {
            _states = new Dictionary<Type, IExitState>();
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

        void IGameStateService.AddState<TState>(TState state)
        {
            _states.Add(typeof(TState), state);
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