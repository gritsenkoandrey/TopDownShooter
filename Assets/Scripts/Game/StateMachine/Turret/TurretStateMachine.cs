using System;
using System.Collections.Generic;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Turret
{
    public sealed class TurretStateMachine : StateMachine
    {
        public TurretStateMachine(CTurret turret)
        {
            States = new Dictionary<Type, IState>
            {
                {typeof(TurretStateDeath), new TurretStateDeath(this, turret)},
                {typeof(TurretStateFight), new TurretStateFight(this, turret)},
                {typeof(TurretStateNone), new TurretStateNone(this, turret)},
                {typeof(TurretStateIdle), new TurretStateIdle(this, turret)},
            };
        }
    }
}