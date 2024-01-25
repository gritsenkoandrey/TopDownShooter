using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateDeath : UnitState, IState
    {
        public UnitStateDeath(IStateMachine stateMachine, CUnit unit, LevelModel levelModel) 
            : base(stateMachine, unit, levelModel)
        {
        }

        public void Enter()
        {
            Unit.Agent.ResetPath();
            Unit.Radar.Clear.Execute();
            Unit.Animator.OnDeath.Execute();
            Unit.CleanSubscribe();
        }

        public void Exit() { }
        public void Tick() { }
    }
}