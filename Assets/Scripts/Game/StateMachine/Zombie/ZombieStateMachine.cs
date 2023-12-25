using System;
using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateMachine : StateMachine
    {
        public ZombieStateMachine(CZombie zombie, LevelModel levelModel)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(ZombieStateNone)] = new ZombieStateNone(this, zombie, levelModel),
                [typeof(ZombieStateIdle)] = new ZombieStateIdle(this, zombie, levelModel),
                [typeof(ZombieStatePatrol)] = new ZombieStatePatrol(this, zombie, levelModel),
                [typeof(ZombieStatePursuit)] = new ZombieStatePursuit(this, zombie, levelModel),
                [typeof(ZombieStateDeath)] = new ZombieStateDeath(this, zombie, levelModel),
                [typeof(ZombieStateFight)] = new ZombieStateFight(this, zombie, levelModel),
            };
        }
    }
}