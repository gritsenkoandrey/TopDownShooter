using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateNone : UnitState, IState
    {
        public UnitStateNone(IStateMachine stateMachine, CUnit unit, LevelModel levelModel) 
            : base(stateMachine, unit, levelModel)
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