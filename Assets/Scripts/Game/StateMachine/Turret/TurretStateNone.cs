using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Turret
{
    public sealed class TurretStateNone : TurretState, IState
    {
        public TurretStateNone(IStateMachine stateMachine, CTurret turret) : base(stateMachine, turret)
        {
        }

        public void Enter()
        {
            Turret.Radar.Clear.Execute();
            Turret.CleanSubscribe();
        }

        public void Exit()
        {
        }

        public void Tick()
        {
        }
    }
}