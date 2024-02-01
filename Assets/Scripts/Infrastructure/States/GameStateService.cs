using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using VContainer;

namespace CodeBase.Infrastructure.States
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class GameStateService : IGameStateService
    {
        private readonly IDictionary<Type, IExitState> _states;

        private IExitState _activeState;

        public GameStateService(IObjectResolver objectResolver)
        {
            _states = new Dictionary<Type, IExitState>
            {
                {typeof(StateBootstrap), new StateBootstrap(this)},
                {typeof(StateFail), new StateFail(this)},
                {typeof(StateGame), new StateGame(this)},
                {typeof(StateLoadLevel), new StateLoadLevel(this)},
                {typeof(StateLoadProgress), new StateLoadProgress(this)},
                {typeof(StateLobby), new StateLobby(this)},
                {typeof(StatePreview), new StatePreview(this)},
                {typeof(StateWin), new StateWin(this)},
            };

            InjectStates(objectResolver);
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

        private TState GetState<TState>() where TState : class, IExitState => _states[typeof(TState)] as TState;

        private void InjectStates(IObjectResolver objectResolver)
        {
            foreach (IExitState state in _states.Values)
            {
                objectResolver.Inject(state);
            }
        }
    }
}