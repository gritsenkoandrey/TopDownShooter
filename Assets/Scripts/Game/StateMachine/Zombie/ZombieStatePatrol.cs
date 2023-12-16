using CodeBase.Game.Components;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePatrol : ZombieState, IState
    {
        private readonly Vector3 _patrolPosition;
        private readonly float _aggroRadius;
        private int _startHealth;

        public ZombieStatePatrol(IStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie)
        {
            _patrolPosition = Zombie.Position;
            _aggroRadius = Mathf.Pow(zombie.Stats.AggroRadius, 2);
        }
        
        void IState.Enter()
        {
            _startHealth = Zombie.Health.CurrentHealth.Value;
            Zombie.Agent.speed = Zombie.Stats.WalkSpeed;
            Zombie.Animator.OnRun.Execute(0f);
            Zombie.Agent.SetDestination(GeneratePointOnNavmesh());
        }

        void IState.Exit()
        {
            Zombie.Agent.ResetPath();
        }

        void IState.Tick()
        {
            if (Distance() < _aggroRadius || IsAggro())
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
                    
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            return new Vector3(x, 0f, z);
        }
        
        private float Distance() => (Zombie.Target.Value.Move.Position - Zombie.Position).sqrMagnitude;
        
        private bool IsAggro() => _startHealth > Zombie.Health.CurrentHealth.Value;
    }
}