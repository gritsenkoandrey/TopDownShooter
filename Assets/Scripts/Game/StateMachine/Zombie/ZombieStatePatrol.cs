using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePatrol : ZombieState, IEnemyState
    {
        private readonly Vector3 _patrolPosition;
        private readonly float _aggroRadius;

        public ZombieStatePatrol(IEnemyStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie)
        {
            _patrolPosition = Zombie.Position;
            _aggroRadius = zombie.Stats.AggroRadius * zombie.Stats.AggroRadius;
        }
        
        void IEnemyState.Enter()
        {
            Zombie.Agent.speed = Zombie.Stats.WalkSpeed;
            Zombie.Animator.Animator.SetFloat(Animations.RunBlend, 0f);
            Zombie.Agent.SetDestination(GeneratePointOnNavmesh());
        }

        void IEnemyState.Exit()
        {
            Zombie.Agent.ResetPath();
        }

        void IEnemyState.Tick()
        {
            if (Distance() < _aggroRadius || Zombie.IsAggro)
            {
                StateMachine.Enter<ZombieStatePursuit>();
            }
            else
            {
                if (Zombie.Agent.hasPath)
                {
                    return;
                }

                StateMachine.Enter<ZombieStateIdle>();
            }
        }
        
        private Vector3 GeneratePointOnNavmesh()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 center = _patrolPosition + GenerateRandomPoint(Zombie.Stats.PatrolRadius);

                if (NavMesh.SamplePosition(center, out NavMeshHit hit, 1f, 1))
                {
                    return hit.position;
                }
            }
            
            return Vector3.zero;
        }
        
        private Vector3 GenerateRandomPoint(float radius)
        {
            float angle = Random.Range(0f, 1f) * (2f * Mathf.PI) - Mathf.PI;
                    
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return new Vector3(x, 0f, z);
        }
        
        private float Distance() => (Zombie.Target.Value.Position - Zombie.Position).sqrMagnitude;
    }
}