using System;
using System.Collections.Generic;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateMachine : StateMachine
    {
        public UnitStateMachine(CUnit unit)
        {
            States = new Dictionary<Type, IState>
            {
                {typeof(UnitStateIdle), new UnitStateIdle(this, unit)},
                {typeof(UnitStateDeath), new UnitStateDeath(this, unit)},
                {typeof(UnitStateFight), new UnitStateFight(this, unit)},
                {typeof(UnitStateNone), new UnitStateNone(this, unit)},
                {typeof(UnitStatePatrol), new UnitStatePatrol(this, unit)},
                {typeof(UnitStatePursuit), new UnitStatePursuit(this, unit)},
            };
        }
    }
}