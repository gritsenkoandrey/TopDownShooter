using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateIdle : CharacterState, IState
    {
        public CharacterStateIdle(IStateMachine stateMachine, ICharacter character, ICameraService cameraService, 
            IJoystickService joystickService, LevelModel levelModel) 
            : base(stateMachine, character, cameraService, joystickService, levelModel)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(0f);
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            UseGravity();
            
            if (HasInput())
            {
                StateMachine.Enter<CharacterStateRun>();
                
                return;
            }

            if (HasDetectedTarget())
            {
                StateMachine.Enter<CharacterStateFight>();
                
                return;
            }
        }

        private void UseGravity()
        {
            if (Character.Move.IsGrounded) return;
            
            Vector3 move = Vector3.zero;
            move.y = Physics.gravity.y;
            Character.Move.CharacterController.Move(move * Character.Move.Speed * Time.deltaTime);
        }

        private bool HasInput()
        {
            return JoystickService.GetAxis().sqrMagnitude > 0.1f;
        }

        private bool HasDetectedTarget()
        {
            for (int i = 0; i < LevelModel.Enemies.Count; i++)
            {
                if (DistanceToTarget(LevelModel.Enemies[i].Position) < Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance())
                {
                    return true;
                }
            }

            return false;
        }
        
        
        private float DistanceToTarget(Vector3 target) => (Character.Move.Position - target).sqrMagnitude;
    }
}