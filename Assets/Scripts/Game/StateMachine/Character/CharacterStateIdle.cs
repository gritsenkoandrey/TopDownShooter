using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateIdle : CharacterState, IState
    {
        public CharacterStateIdle(IStateMachine stateMachine, ICharacter character, ICameraService cameraService, 
            IJoystickService joystickService, IGameFactory gameFactory) 
            : base(stateMachine, character, cameraService, joystickService, gameFactory)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(0f);
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            if (JoystickService.GetAxis().sqrMagnitude > 0.1f)
            {
                StateMachine.Enter<CharacterStateRun>();
                
                return;
            }

            Gravity();

            if (HasDetectedTarget())
            {
                StateMachine.Enter<CharacterStateFight>();
            }
        }

        private void Gravity()
        {
            Vector3 move = Vector3.zero;
            move.y = Character.Move.IsGrounded ? 0f : Physics.gravity.y;
            Character.Move.CharacterController.Move(move * Character.Move.Speed * Time.deltaTime);
        }
        
        private bool HasDetectedTarget()
        {
            for (int i = 0; i < GameFactory.Enemies.Count; i++)
            {
                if (Distance(GameFactory.Enemies[i].Position) < Character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance())
                {
                    return true;
                }
            }

            return false;
        }
        
        private float Distance(Vector3 target) => (Character.Move.Position - target).sqrMagnitude;
    }
}