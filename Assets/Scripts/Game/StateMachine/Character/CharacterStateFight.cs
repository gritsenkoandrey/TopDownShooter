using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateFight : CharacterState, IState
    {
        private IEnemy _target;
        
        public CharacterStateFight(IStateMachine stateMachine, CCharacter character, ICameraService cameraService, 
            IJoystickService joystickService, LevelModel levelModel) 
            : base(stateMachine, character, cameraService, joystickService, levelModel)
        {
        }

        void IState.Enter() { }

        void IState.Exit() { }

        void IState.Tick()
        {
            UseGravity();
            
            if (HasInput())
            {
                StateMachine.Enter<CharacterStateRun>();
                
                return;
            }

            if (TrySetTarget())
            {
                LockAtTarget();

                if (Character.WeaponMediator.CurrentWeapon.Weapon.CanAttack())
                {
                    Attack();
                }
            }
            else
            {
                StateMachine.Enter<CharacterStateIdle>();
            }
        }

        private void UseGravity()
        {
            if (Character.Move.IsGrounded) return;
            
            Vector3 move = Vector3.zero;
            move.y = Physics.gravity.y;
            Character.Move.CharacterController.Move(move * Character.Move.Speed * Time.deltaTime);
        }

        private void Attack()
        {
            Character.Animator.OnAttack.Execute();
            Character.WeaponMediator.CurrentWeapon.Weapon.Attack(_target);
        }

        private bool HasInput()
        {
            return JoystickService.GetAxis().sqrMagnitude > 0.1f;
        }

        private void LockAtTarget()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_target.Position - Character.Position);
            Character.Move.transform.rotation = lookRotation;
        }

        private bool TrySetTarget()
        {
            if (LevelModel.Enemies.Count == 0)
            {
                return false;
            }

            int index = FindNearestTargetIndex();

            if (index >= 0)
            {
                _target = LevelModel.Enemies[index];
                
                return true;
            }

            return false;
        }
        
        private int FindNearestTargetIndex()
        {
            int index = -1;
            
            float minDistance = Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance();

            for (int i = 0; i < LevelModel.Enemies.Count; i++)
            {
                float distance = DistanceToTarget(LevelModel.Enemies[i].Position);

                if (distance < Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance())
                {
                    if (distance < minDistance && HasObstacleOnAttackPath(LevelModel.Enemies[i].Position) == false)
                    {
                        index = i;
                        minDistance = distance;
                    }
                }
            }

            return index;
        }

        private float DistanceToTarget(Vector3 target) => (Character.Position - target).sqrMagnitude;

        private bool HasObstacleOnAttackPath(Vector3 target)
        {
            if (Character.WeaponMediator.CurrentWeapon.Weapon.IsDetectThroughObstacle() == false)
            {
                return false;
            }

            return Physics.Linecast(Character.Position, target, Layers.Wall);
        }
    }
}