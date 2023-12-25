using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateDeath : ZombieState, IState
    {
        public ZombieStateDeath(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel) { }

        void IState.Enter()
        {
            Zombie.Agent.ResetPath();
            Zombie.Radar.Clear.Execute();
            Zombie.Animator.OnDeath.Execute();
            Zombie.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}