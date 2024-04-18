using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Turret
{
    public abstract class TurretState
    {
        private readonly IStateMachine _stateMachine;
        
        protected CTurret Turret { get; }

        protected TurretState(IStateMachine stateMachine, CTurret turret)
        {
            _stateMachine = stateMachine;
            Turret = turret;
        }
        
        protected void EnterState<T>() where T : TurretState, IState => _stateMachine.Enter<T>();
    }
}