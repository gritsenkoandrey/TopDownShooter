using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Zombie
{
    public abstract class ZombieState
    {
        protected readonly IStateMachine StateMachine;
        protected readonly CZombie Zombie;
        protected readonly LevelModel LevelModel;

        protected ZombieState(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel)
        {
            StateMachine = stateMachine;
            Zombie = zombie;
            LevelModel = levelModel;
        }
    }
}