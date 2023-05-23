using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePatrol : ZombieState
    {
        private readonly CZombie _zombie;
        private readonly Vector3 _patrolPosition;

        public ZombieStatePatrol(ZombieStateMachine stateMachine, CZombie zombie) : base(stateMachine)
        {
            _zombie = zombie;
            _patrolPosition = _zombie.transform.position;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _zombie.Agent.speed = _zombie.Stats.WalkSpeed;
            _zombie.Animator.Animator.SetFloat(Animations.RunBlend, 0f);
            _zombie.Agent.SetDestination(GeneratePointOnNavmesh());
        }

        public override void Exit()
        {
            base.Exit();
            
            _zombie.Agent.ResetPath();
        }

        public override void Tick()
        {
            base.Tick();
            
            if (Distance() < _zombie.Radar.Radius || _zombie.IsAggro)
            {
                StateMachine.Enter<ZombieStatePursuit>();
            }
            else
            {
                if (_zombie.Agent.hasPath)
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
                Vector3 center = _patrolPosition + GenerateRandomPoint(_zombie.Stats.PatrolRadius);

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
        
        private float Distance() => Vector3.Distance(_zombie.Position, _zombie.Character.Position);
    }
}