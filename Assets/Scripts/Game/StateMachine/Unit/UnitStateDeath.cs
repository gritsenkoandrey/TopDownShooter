using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateDeath : UnitState, IState
    {

        public UnitStateDeath(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }
        
        public void Enter()
        {
            Unit.Agent.Agent.ResetPath();
            Unit.Animator.OnDeath.Execute();
            Unit.CleanSubscribe();
        }

        public void Exit() { }

        public void Tick() { }
    }
}