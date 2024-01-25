using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Unit
{
    public abstract class UnitState
    {
        private protected readonly IStateMachine StateMachine;
        private protected readonly CUnit Unit;
        private protected readonly LevelModel LevelModel;

        private protected UnitState(IStateMachine stateMachine, CUnit unit, LevelModel levelModel)
        {
            StateMachine = stateMachine;
            Unit = unit;
            LevelModel = levelModel;
        }
    }
}