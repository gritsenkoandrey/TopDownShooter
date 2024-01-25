using System;
using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateMachine : StateMachine
    {
        public UnitStateMachine(CUnit unit, LevelModel levelModel)
        {
            UnitStateIdle unitStateIdle = new UnitStateIdle(this, unit, levelModel);
            UnitStateDeath unitStateDeath = new UnitStateDeath(this, unit, levelModel);
            UnitStateFight unitStateFight = new UnitStateFight(this, unit, levelModel);
            UnitStateNone unitStateNone = new UnitStateNone(this, unit, levelModel);
            UnitStatePatrol unitStatePatrol = new UnitStatePatrol(this, unit, levelModel);
            UnitStatePursuit unitStatePursuit = new UnitStatePursuit(this, unit, levelModel);
            
            States = new Dictionary<Type, IState>
            {
                [typeof(UnitStateIdle)] = unitStateIdle,
                [typeof(UnitStateDeath)] = unitStateDeath,
                [typeof(UnitStateFight)] = unitStateFight,
                [typeof(UnitStateNone)] = unitStateNone,
                [typeof(UnitStatePatrol)] = unitStatePatrol,
                [typeof(UnitStatePursuit)] = unitStatePursuit,
            };
        }
    }
}