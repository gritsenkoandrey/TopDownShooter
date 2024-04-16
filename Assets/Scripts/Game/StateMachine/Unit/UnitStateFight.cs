using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateFight : UnitState, IState
    {
        private LevelModel _levelModel;

        private float _attackDistance;

        public UnitStateFight(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _attackDistance = Unit.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
            
            Unit.Agent.Agent.ResetPath();
            Unit.Animator.OnRun.Execute(0f);
        }

        public void Exit() { }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<UnitStateDeath>();
                
                return;
            }
            
            if (CannotAttack())
            {
                EnterState<UnitStatePursuit>();

                return;
            }
            
            LookAt();
            
            if (CanAttack())
            {
                Unit.WeaponMediator.CurrentWeapon.Weapon.Attack(_levelModel.Character);
                Unit.Animator.OnAttack.Execute();
            }
        }
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        
        private bool HasObstacleOnAttackPath()
        {
            if (Unit.WeaponMediator.CurrentWeapon.Weapon.IsDetectThroughObstacle() == false)
            {
                return false;
            }
            return Physics.Linecast(Unit.Position, _levelModel.Character.Position, Layers.Wall);
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_levelModel.Character.Position - Unit.Position);

            Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, Unit.WeaponMediator.CurrentWeapon.Weapon.AimingSpeed());
        }
        
        private bool HasFacingTarget()
        {
            float angle = Vector3.Angle(Unit.Forward.normalized, (_levelModel.Character.Position - Unit.Position).normalized);

            return angle < 5f;
        }

        private bool IsDeath() => Unit.Health.IsAlive == false;

        private bool CanAttack()
        {
            return Unit.WeaponMediator.CurrentWeapon.Weapon.CanAttack() && 
                   HasObstacleOnAttackPath() == false &&
                   HasFacingTarget();
        }

        private bool CannotAttack()
        {
            return DistanceToTarget() > _attackDistance && _levelModel.Character.Health.IsAlive ||
                   HasObstacleOnAttackPath();
        }
    }
}