using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePursuit : ZombieState, IEnemyState
    {
        private float _attackDelay;
        private readonly float _pursuitRadius;
        private readonly float _minDistanceToTarget;

        public ZombieStatePursuit(IEnemyStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie)
        {
            _pursuitRadius = zombie.Stats.PursuitRadius * zombie.Stats.PursuitRadius;
            _minDistanceToTarget = zombie.Stats.MinDistanceToTarget * zombie.Stats.MinDistanceToTarget;
        }
        
        void IEnemyState.Enter()
        {
            _attackDelay = Zombie.Stats.AttackDelay;
            Zombie.Agent.speed = Zombie.Stats.RunSpeed;
            Zombie.Animator.Animator.SetFloat(Animations.RunBlend, 1f);
            Zombie.Radar.Clear.Execute();
        }

        void IEnemyState.Exit()
        {
            Zombie.Agent.ResetPath();
        }

        void IEnemyState.Tick()
        {
            if (Distance() > _pursuitRadius)
            {
                Zombie.IsAggro = false;
                
                StateMachine.Enter<ZombieStateIdle>();
            }
            else
            {
                LookAt();
                
                if (Distance() < _minDistanceToTarget)
                {
                    if (Zombie.Agent.hasPath)
                    {
                        Zombie.Agent.ResetPath();
                    }
                    
                    Attack();
                }
                else
                {
                    Zombie.Agent.SetDestination(Zombie.Target.Value.Position);
                }

                _attackDelay -= Time.deltaTime;
            }
        }
        
        private void Attack()
        {
            if (_attackDelay < 0f)
            {
                _attackDelay = Zombie.Stats.AttackDelay;

                Zombie.Melee.Attack.Execute();
            }
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(Zombie.Target.Value.Position - Zombie.Position);

            Zombie.transform.rotation = Quaternion.Slerp(Zombie.transform.rotation, lookRotation, 0.5f);
        }
        
        private float Distance() => (Zombie.Target.Value.Position - Zombie.Position).sqrMagnitude;
    }
}