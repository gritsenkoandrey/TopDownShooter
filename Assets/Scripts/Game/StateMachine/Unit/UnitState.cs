using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Unit
{
    public abstract class UnitState
    {
        private readonly IStateMachine _stateMachine;
        
        protected CUnit Unit { get; }

        protected UnitState(IStateMachine stateMachine, CUnit unit)
        {
            _stateMachine = stateMachine;
            Unit = unit;
        }
        
        protected void EnterState<T>() where T : UnitState, IState => _stateMachine.Enter<T>();
    }
}