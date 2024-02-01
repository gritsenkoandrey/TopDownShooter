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
            States = new Dictionary<Type, IState>
            {
                {typeof(UnitStateIdle), new UnitStateIdle(this, unit, levelModel)},
                {typeof(UnitStateDeath), new UnitStateDeath(this, unit, levelModel)},
                {typeof(UnitStateFight), new UnitStateFight(this, unit, levelModel)},
                {typeof(UnitStateNone), new UnitStateNone(this, unit, levelModel)},
                {typeof(UnitStatePatrol), new UnitStatePatrol(this, unit, levelModel)},
                {typeof(UnitStatePursuit), new UnitStatePursuit(this, unit, levelModel)},
            };
        }
    }
}