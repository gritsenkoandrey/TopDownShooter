using System;
using System.Collections.Generic;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateMachine
    {
        private ZombieState _activeState;
        
        private readonly Dictionary<Type, ZombieState> _states;

        public ZombieStateMachine(CZombie zombie)
        {
            _states = new Dictionary<Type, ZombieState>
            {
                [typeof(ZombieStateNone)] = new ZombieStateNone(this, zombie),
                [typeof(ZombieStateIdle)] = new ZombieStateIdle(this, zombie),
                [typeof(ZombieStatePatrol)] = new ZombieStatePatrol(this, zombie),
                [typeof(ZombieStatePursuit)] = new ZombieStatePursuit(this, zombie),
            };
        }

        public void Enter<T>() where T : ZombieState
        {
            T type = _states[typeof(T)] as T;

            _activeState?.Exit();
            _activeState = type;
            _activeState?.Enter();
        }

        public void Tick() => _activeState?.Tick();
    }
}