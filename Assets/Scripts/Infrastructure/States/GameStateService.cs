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
            StateBootstrap stateBootstrap = new StateBootstrap(this);
            StateFail stateFail = new StateFail(this);
            StateGame stateGame = new StateGame(this);
            StateLoadLevel stateLoadLevel = new StateLoadLevel(this);
            StateLoadProgress stateLoadProgress = new StateLoadProgress(this);
            StateLobby stateLobby = new StateLobby(this);
            StatePreview statePreview = new StatePreview(this);
            StateWin stateWin = new StateWin(this);
            
            _states = new Dictionary<Type, IExitState>
            {
                {typeof(StateBootstrap), stateBootstrap},
                {typeof(StateFail), stateFail},
                {typeof(StateGame), stateGame},
                {typeof(StateLoadLevel), stateLoadLevel},
                {typeof(StateLoadProgress), stateLoadProgress},
                {typeof(StateLobby), stateLobby},
                {typeof(StatePreview), statePreview},
                {typeof(StateWin), stateWin},
            };
            
            objectResolver.Inject(stateBootstrap);
            objectResolver.Inject(stateFail);
            objectResolver.Inject(stateGame);
            objectResolver.Inject(stateLoadLevel);
            objectResolver.Inject(stateLoadProgress);
            objectResolver.Inject(stateLobby);
            objectResolver.Inject(statePreview);
            objectResolver.Inject(stateWin);
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
    }
}