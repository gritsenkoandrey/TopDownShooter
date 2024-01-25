using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStatePursuit : UnitState, IState
    {
        private float _pursuitRadius;
        private float _attackDistance;
        
        public UnitStatePursuit(IStateMachine stateMachine, CUnit unit, LevelModel levelModel) 
            : base(stateMachine, unit, levelModel)
        {
        }

        public void Enter()
        {
            _pursuitRadius = Mathf.Pow(Unit.UnitStats.PursuitRadius, 2);
            _attackDistance = Unit.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();
            Unit.Agent.speed = Unit.UnitStats.PursuitSpeed;
            Unit.Animator.OnRun.Execute(1f);
            Unit.Radar.Clear.Execute();
        }

        public void Exit()
        {
            Unit.Agent.ResetPath();
        }

        public void Tick()
        {
            if (DistanceToTarget() > _pursuitRadius)
            {
                StateMachine.Enter<UnitStateIdle>();
            }
            else
            {
                LookAt();
                
                if (DistanceToTarget() < _attackDistance)
                {
                    StateMachine.Enter<UnitStateFight>();
                }
                else
                {
                    Unit.Agent.SetDestination(LevelModel.Character.Move.Position);
                }
            }
        }
        
        private float DistanceToTarget() => (LevelModel.Character.Move.Position - Unit.Position).sqrMagnitude;

        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(LevelModel.Character.Move.Position - Unit.Position);

            Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, 0.5f);
        }
    }
}