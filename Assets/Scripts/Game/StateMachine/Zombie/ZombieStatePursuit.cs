using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePursuit : ZombieState, IState
    {
        private readonly float _pursuitRadius;
        private readonly float _minDistanceToTarget;

        public ZombieStatePursuit(IStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie)
        {
            _pursuitRadius = Mathf.Pow(zombie.Stats.PursuitRadius, 2);
            _minDistanceToTarget = Mathf.Pow(zombie.Stats.MinDistanceToTarget, 2);
        }
        
        void IState.Enter()
        {
            Zombie.Agent.speed = Zombie.Stats.RunSpeed;
            Zombie.Animator.OnRun.Execute(1f);
            Zombie.Radar.Clear.Execute();
        }

        void IState.Exit()
        {
            Zombie.Agent.ResetPath();
        }

        void IState.Tick()
        {
            if (DistanceToTarget() > _pursuitRadius)
            {
                StateMachine.Enter<ZombieStateIdle>();
            }
            else
            {
                LookAt();
                
                if (DistanceToTarget() < _minDistanceToTarget)
                {
                    StateMachine.Enter<ZombieStateFight>();
                }
                else
                {
                    Zombie.Agent.SetDestination(Zombie.Target.Value.Move.Position);
                }
            }
        }

        private float DistanceToTarget() => (Zombie.Target.Value.Move.Position - Zombie.Position).sqrMagnitude;

        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(Zombie.Target.Value.Move.Position - Zombie.Position);

            Zombie.transform.rotation = Quaternion.Slerp(Zombie.transform.rotation, lookRotation, 0.5f);
        }
    }
}