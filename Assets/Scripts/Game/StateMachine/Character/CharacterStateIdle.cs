using CodeBase.Game.Components;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateIdle : CharacterState, IState
    {
        private readonly IJoystickService _joystickService;
        private readonly LevelModel _levelModel;
        
        public CharacterStateIdle(IStateMachine stateMachine, CCharacter character, IJoystickService joystickService, LevelModel levelModel) 
            : base(stateMachine, character)
        {
            _joystickService = joystickService;
            _levelModel = levelModel;
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
            return _joystickService.GetAxis().sqrMagnitude > 0.1f;
        }

        private bool HasDetectedTarget()
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                if (DistanceToTarget(_levelModel.Enemies[i].Position) < Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance())
                {
                    return true;
                }
            }

            return false;
        }
        
        
        private float DistanceToTarget(Vector3 target) => (Character.Position - target).sqrMagnitude;
    }
}