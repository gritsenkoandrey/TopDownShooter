using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStatePatrol : UnitState, IState
    {
        private LevelModel _levelModel;

        private readonly Vector3 _patrolPosition;
        private float _aggroRadius;
        private float _patrolRadius;
        private int _startHealth;

        public UnitStatePatrol(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
            _patrolPosition = Unit.Position;
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _patrolRadius = Unit.UnitStats.PatrolRadius;
            _aggroRadius = Mathf.Pow(Unit.WeaponMediator.CurrentWeapon.Weapon.DetectionDistance(), 2);
            _startHealth = Unit.Health.CurrentHealth.Value;
            Unit.Agent.Agent.speed = Unit.UnitStats.WalkSpeed;
            Unit.Animator.OnRun.Execute(1f);
            Unit.Agent.Agent.SetDestination(GeneratePointOnNavmesh());
        }

        public void Exit()
        {
            Unit.Agent.Agent.ResetPath();
        }

        public void Tick()
        {
            if (Unit.Health.IsAlive == false)
            {
                StateMachine.Enter<UnitStateDeath>();
                
                return;
            }

            if (DistanceToTarget() < _aggroRadius || IsAggro())
            {
                StateMachine.Enter<UnitStatePursuit>();
            }
            else
            {
                if (Unit.Agent.Agent.hasPath)
                {
                    return;
                }

                StateMachine.Enter<UnitStateIdle>();
            }
        }

        private Vector3 GeneratePointOnNavmesh()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 center = _patrolPosition + Mathematics.GenerateRandomPoint(_patrolRadius);

                if (NavMesh.SamplePosition(center, out NavMeshHit hit, 1f, 1))
                {
                    return hit.position;
                }
            }
            
            return Vector3.zero;
        }

        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;

        private bool IsAggro() => _startHealth > Unit.Health.CurrentHealth.Value;
    }
}