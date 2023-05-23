using System;
using System.Collections.Generic;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateMachine : IEnemyStateMachine
    {
        private IEnemyState _activeState;
        
        private readonly Dictionary<Type, IEnemyState> _states;

        public ZombieStateMachine(CZombie zombie)
        {
            _states = new Dictionary<Type, IEnemyState>
            {
                [typeof(ZombieStateNone)] = new ZombieStateNone(this, zombie),
                [typeof(ZombieStateIdle)] = new ZombieStateIdle(this, zombie),
                [typeof(ZombieStatePatrol)] = new ZombieStatePatrol(this, zombie),
                [typeof(ZombieStatePursuit)] = new ZombieStatePursuit(this, zombie),
            };
        }

        void IEnemyStateMachine.Enter<T>()
        {
            T state = ChangeState<T>();

            state.Enter();
        }

        void IEnemyStateMachine.Tick() => _activeState?.Tick();

        private T ChangeState<T>() where T : class, IEnemyState
        {
            _activeState?.Exit();

            T state = GetState<T>();

            _activeState = state;
            
            return state;
        }

        private T GetState<T>() where T : class, IEnemyState
        {
            return _states[typeof(T)] as T;
        }
    }
}