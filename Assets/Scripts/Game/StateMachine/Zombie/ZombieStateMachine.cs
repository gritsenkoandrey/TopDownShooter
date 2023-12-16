using System;
using System.Collections.Generic;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateMachine : StateMachine
    {
        public ZombieStateMachine(CZombie zombie)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(ZombieStateNone)] = new ZombieStateNone(this, zombie),
                [typeof(ZombieStateIdle)] = new ZombieStateIdle(this, zombie),
                [typeof(ZombieStatePatrol)] = new ZombieStatePatrol(this, zombie),
                [typeof(ZombieStatePursuit)] = new ZombieStatePursuit(this, zombie),
                [typeof(ZombieStateDeath)] = new ZombieStateDeath(this, zombie),
                [typeof(ZombieStateFight)] = new ZombieStateFight(this, zombie),
            };
        }
    }
}