using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Zombie
{
    public abstract class ZombieState
    {
        protected readonly IStateMachine StateMachine;
        protected readonly CZombie Zombie;

        protected ZombieState(IStateMachine stateMachine, CZombie zombie)
        {
            StateMachine = stateMachine;
            Zombie = zombie;
        }
    }
}