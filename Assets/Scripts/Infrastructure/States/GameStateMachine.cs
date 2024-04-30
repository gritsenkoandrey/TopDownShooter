using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        public IReadOnlyDictionary<Type, IExitState> States { get; }

        private IExitState _activeState;

        public GameStateMachine()
        {
            States = new Dictionary<Type, IExitState>
            {
                {typeof(StateBootstrap), new StateBootstrap(this)},
                {typeof(StateResult), new StateResult(this)},
                {typeof(StateGame), new StateGame(this)},
                {typeof(StateLoadLevel), new StateLoadLevel(this)},
                {typeof(StateLoadProgress), new StateLoadProgress(this)},
                {typeof(StateLobby), new StateLobby(this)},
                {typeof(StatePreview), new StatePreview(this)}
            };
        }

        void IGameStateMachine.Enter<TState>()
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        void IGameStateMachine.Enter<TState, TLoad>(TLoad load)
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

        private TState GetState<TState>() where TState : class, IExitState => States[typeof(TState)] as TState;
    }
}