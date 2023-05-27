using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateNone : ZombieState, IEnemyState
    {
        public ZombieStateNone(IEnemyStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie) { }

        void IEnemyState.Enter()
        {
            Zombie.Agent.ResetPath();
            Zombie.Radar.Clear.Execute();
            Zombie.Health.Collider.enabled = false;
        }

        void IEnemyState.Exit() { }

        void IEnemyState.Tick() { }
    }
}