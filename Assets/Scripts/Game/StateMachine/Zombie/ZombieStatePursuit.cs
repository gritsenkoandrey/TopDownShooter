using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Models;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePursuit : ZombieState, IState
    {
        private float _pursuitRadius;
        private float _minDistanceToTarget;
        private ICharacter _target;

        public ZombieStatePursuit(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel) { }
        
        void IState.Enter()
        {
            _target = LevelModel.Character;
            _pursuitRadius = Mathf.Pow(Zombie.Stats.PursuitRadius, 2);
            _minDistanceToTarget = Mathf.Pow(Zombie.Stats.MinDistanceToTarget, 2);
            Zombie.Agent.speed = Zombie.Stats.RunSpeed;
            Zombie.Animator.OnRun.Execute(1f);
            Zombie.Radar.Clear.Execute();
        }

        void IState.Exit()
        {
            _target = null;
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
                    Zombie.Agent.SetDestination(_target.Move.Position);
                }
            }
        }

        private float DistanceToTarget() => (_target.Move.Position - Zombie.Position).sqrMagnitude;

        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_target.Move.Position - Zombie.Position);

            Zombie.transform.rotation = Quaternion.Slerp(Zombie.transform.rotation, lookRotation, 0.5f);
        }
    }
}