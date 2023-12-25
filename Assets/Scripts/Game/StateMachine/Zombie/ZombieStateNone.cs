using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateNone : ZombieState, IState
    {
        public ZombieStateNone(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel) { }

        void IState.Enter()
        {
            Zombie.Agent.ResetPath();
            Zombie.Radar.Clear.Execute();
            Zombie.Animator.OnIdle.Execute();
            Zombie.CleanSubscribe();
        }

        void IState.Exit() { }

        void IState.Tick() { }
    }
}