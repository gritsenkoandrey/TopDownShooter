using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateFight : UnitState, IState
    {
        private float _attackDistance;

        public UnitStateFight(IStateMachine stateMachine, CUnit unit, LevelModel levelModel) 
            : base(stateMachine, unit, levelModel)
        {
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
            if (DistanceToTarget() > _attackDistance && LevelModel.Character.Health.IsAlive)
            {
                StateMachine.Enter<UnitStatePursuit>();
                
                return;
            }
            
            LookAt();
            
            if (Unit.WeaponMediator.CurrentWeapon.Weapon.CanAttack() && HasObstacleOnAttackPath() == false)
            {
                Unit.WeaponMediator.CurrentWeapon.Weapon.Attack(LevelModel.Character);
                Unit.Animator.OnAttack.Execute();
            }
        }
        
        private float DistanceToTarget() => (LevelModel.Character.Position - Unit.Position).sqrMagnitude;
        
        private bool HasObstacleOnAttackPath()
        {
            if (Unit.WeaponMediator.CurrentWeapon.Weapon.IsDetectThroughObstacle() == false)
            {
                return false;
            }

            return Physics.Linecast(Unit.Position, LevelModel.Character.Position, Layers.Wall);
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(LevelModel.Character.Position - Unit.Position);

            Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, 0.5f);
        }
    }
}