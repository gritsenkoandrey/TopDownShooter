using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public abstract class ZombieState
    {
        protected IEnemyStateMachine StateMachine { get; }
        protected CZombie Zombie { get; }

        protected ZombieState(IEnemyStateMachine stateMachine, CZombie zombie)
        {
            StateMachine = stateMachine;
            Zombie = zombie;
        }
    }
}