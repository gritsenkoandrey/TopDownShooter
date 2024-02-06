using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateNone : UnitState, IState
    {
        public UnitStateNone(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }

        public void Enter()
        {
            Unit.Agent.ResetPath();
            Unit.Radar.Clear.Execute();
            Unit.Animator.OnRun.Execute(0f);
            Unit.CleanSubscribe();
        }

        public void Exit() { }
        public void Tick() { }
    }
}