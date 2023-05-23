using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateNone : ZombieState
    {
        private readonly CZombie _zombie;

        public ZombieStateNone(ZombieStateMachine stateMachine, CZombie zombie) : base(stateMachine)
        {
            _zombie = zombie;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _zombie.Agent.ResetPath();
            _zombie.Radar.Clear.Execute();
            _zombie.LifetimeDisposable.Clear();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Tick()
        {
            base.Tick();
        }
    }
}