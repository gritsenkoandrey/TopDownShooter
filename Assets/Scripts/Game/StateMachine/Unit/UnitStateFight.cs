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
            
            Unit.Agent.ResetPath();
            Unit.Animator.OnRun.Execute(0f);
        }

        public void Exit() { }

        public void Tick()
        {
            if (DistanceToTarget() > _attackDistance && _levelModel.Character.Health.IsAlive)
            {
                StateMachine.Enter<UnitStatePursuit>();
                
                return;
            }
            
            LookAt();
            
            if (Unit.WeaponMediator.CurrentWeapon.Weapon.CanAttack() && HasObstacleOnAttackPath() == false)
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

            Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, LerpRotate);
        }
    }
}