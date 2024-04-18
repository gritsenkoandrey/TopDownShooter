using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Turret
{
    public sealed class TurretStateFight : TurretState, IState
    {
        private LevelModel _levelModel;

        public TurretStateFight(IStateMachine stateMachine, CTurret turret) : base(stateMachine, turret)
        {
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<TurretStateDeath>();
                
                return;
            }

            if (CannotAttack())
            {
                EnterState<TurretStateIdle>();
                
                return;
            }
            
            LookAt();

            if (CanAttack())
            {
                Turret.Weapon.Weapon.Attack(_levelModel.Character);
            }
        }
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Turret.Position).sqrMagnitude;
        
        private bool HasObstacleOnAttackPath()
        {
            if (Turret.Weapon.Weapon.IsDetectThroughObstacle() == false)
            {
                return false;
            }
            
            return Physics.Linecast(Turret.Position, _levelModel.Character.Position, Layers.Wall);
        }
        
        private void LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_levelModel.Character.Position - Turret.Position);

            Turret.Rotate.rotation = Quaternion.Slerp(Turret.Rotate.rotation, lookRotation, Turret.Weapon.Weapon.AimingSpeed());
        }
        
        private bool HasFacingTarget()
        {
            float angle = Vector3.Angle(Turret.Rotate.forward.normalized, (_levelModel.Character.Position - Turret.Position).normalized);

            return angle < 2.5f;
        }

        private bool IsDeath() => Turret.Health.IsAlive == false;

        private bool CanAttack()
        {
            return Turret.Weapon.Weapon.CanAttack() && HasObstacleOnAttackPath() == false && HasFacingTarget();
        }

        private bool CannotAttack()
        {
            return DistanceToTarget() > Turret.Weapon.Weapon.AttackDistance() && _levelModel.Character.Health.IsAlive;
        }
    }
}