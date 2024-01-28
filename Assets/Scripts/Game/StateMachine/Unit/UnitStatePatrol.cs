using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStatePatrol : UnitState, IState
    {
        private readonly Vector3 _patrolPosition;
        private float _aggroRadius;
        private float _patrolRadius;
        private int _startHealth;

        public UnitStatePatrol(IStateMachine stateMachine, CUnit unit, LevelModel levelModel) 
            : base(stateMachine, unit, levelModel)
        {
            _patrolPosition = Unit.Position;
        }

        public void Enter()
        {
            _patrolRadius = Unit.UnitStats.PatrolRadius;
            _aggroRadius = Mathf.Pow(Unit.WeaponMediator.CurrentWeapon.Weapon.DetectionDistance(), 2);
            _startHealth = Unit.Health.CurrentHealth.Value;
            Unit.Agent.speed = Unit.UnitStats.WalkSpeed;
            Unit.Animator.OnRun.Execute(1f);
            Unit.Agent.SetDestination(GeneratePointOnNavmesh());
        }

        public void Exit()
        {
            Unit.Agent.ResetPath();
        }

        public void Tick()
        {
            if (DistanceToTarget() < _aggroRadius || IsAggro())
            {
                StateMachine.Enter<UnitStatePursuit>();
            }
            else
            {
                if (Unit.Agent.hasPath)
                {
                    return;
                }

                StateMachine.Enter<UnitStateIdle>();
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
                Vector3 center = _patrolPosition + GenerateRandomPoint(_patrolRadius);

                if (NavMesh.SamplePosition(center, out NavMeshHit hit, 1f, 1))
                {
                    return hit.position;
                }
            }
            
            return Vector3.zero;
        }

        private float DistanceToTarget() => (LevelModel.Character.Position - Unit.Position).sqrMagnitude;

        private bool IsAggro() => _startHealth > Unit.Health.CurrentHealth.Value;
    }
}