using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Models;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePatrol : ZombieState, IState
    {
        private readonly Vector3 _patrolPosition;
        private float _aggroRadius;
        private ICharacter _target;
        private int _startHealth;

        public ZombieStatePatrol(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel)
        {
            _patrolPosition = zombie.Position;
        }
        
        void IState.Enter()
        {
            _aggroRadius = Mathf.Pow(Zombie.Stats.AggroRadius, 2);
            _target = LevelModel.Character;
            _startHealth = Zombie.Health.CurrentHealth.Value;
            Zombie.Agent.speed = Zombie.Stats.WalkSpeed;
            Zombie.Animator.OnRun.Execute(0f);
            Zombie.Agent.SetDestination(GeneratePointOnNavmesh());
        }

        void IState.Exit()
        {
            _target = null;
            Zombie.Agent.ResetPath();
        }

        void IState.Tick()
        {
            if (DistanceToTarget() < _aggroRadius || IsAggro())
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

        private Vector3 GenerateRandomPoint(float radius)
        {
            float angle = Random.Range(0f, 1f) * (2f * Mathf.PI) - Mathf.PI;
                    
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            return new Vector3(x, 0f, z);
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

        private float DistanceToTarget() => (_target.Move.Position - Zombie.Position).sqrMagnitude;

        private bool IsAggro() => _startHealth > Zombie.Health.CurrentHealth.Value;
    }
}