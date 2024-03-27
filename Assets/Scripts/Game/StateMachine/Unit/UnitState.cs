using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Unit
{
    public abstract class UnitState
    {
        private protected readonly IStateMachine StateMachine;
        private protected readonly CUnit Unit;

        private protected UnitState(IStateMachine stateMachine, CUnit unit)
        {
            StateMachine = stateMachine;
            Unit = unit;
        }
    }
}