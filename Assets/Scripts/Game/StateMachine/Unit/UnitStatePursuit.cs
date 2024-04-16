using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStatePursuit : UnitState, IState
    {
        private LevelModel _levelModel;

        private float _pursuitRadius;
        private float _attackDistance;
        
        public UnitStatePursuit(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _pursuitRadius = Mathf.Pow(Unit.UnitStats.PursuitRadius, 2);
            _attackDistance = Unit.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
            Unit.Agent.Agent.speed = Unit.UnitStats.PursuitSpeed;
            Unit.Animator.OnRun.Execute(1f);
        }

        public void Exit()
        {
            Unit.Agent.Agent.ResetPath();
        }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<UnitStateDeath>();
                
                return;
            }

            if (CanIdle())
            {
                EnterState<UnitStateIdle>();
            }
            else
            {
                if (CanFight())
                {
                    EnterState<UnitStateFight>();
                }
                else
                {
                    Unit.Agent.Agent.SetDestination(_levelModel.Character.Position);
                }
            }
        }

        private bool HasObstacleOnAttackPath()
        {
            if (Unit.WeaponMediator.CurrentWeapon.Weapon.IsDetectThroughObstacle() == false)
            {
                return false;
            }
            
            return Physics.Linecast(Unit.Position, _levelModel.Character.Position, Layers.Wall);
        }

        private bool CanIdle() => DistanceToTarget() > _pursuitRadius;
        private bool CanFight() => DistanceToTarget() < _attackDistance && HasObstacleOnAttackPath() == false;
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        private bool IsDeath() => Unit.Health.IsAlive == false;
    }
}