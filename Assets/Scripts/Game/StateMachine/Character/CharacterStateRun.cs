using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateRun : CharacterState, IState
    {
        private float _angle;

        public CharacterStateRun(IStateMachine stateMachine, ICharacter character, ICameraService cameraService, 
            IJoystickService joystickService, IGameFactory gameFactory) 
            : base(stateMachine, character, cameraService, joystickService, gameFactory)
        {
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(1f);
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            if (HasNoInput())
            {
                StateMachine.Enter<CharacterStateIdle>();
                
                return;
            }

            Move();
            Rotate();
        }

        private bool HasNoInput()
        {
            return JoystickService.GetAxis().sqrMagnitude < 0.1f;
        }

        private void Move()
        {
            _angle = Mathf.Atan2(JoystickService.GetAxis().x, JoystickService.GetAxis().y) * 
                Mathf.Rad2Deg + CameraService.Camera.transform.eulerAngles.y;

            Vector3 move = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;

            Vector3 next = Character.Move.Position + move * Character.Move.Speed * Time.deltaTime;
                        
            Ray ray = new Ray { origin = next, direction = Vector3.down };

            if (!Physics.Raycast(ray, 5f, Layers.Ground))
            {
                return;
            }
                
            move.y = Character.Move.IsGrounded ? 0f : Physics.gravity.y;

            Character.Move.CharacterController.Move(move * Character.Move.Speed * Time.deltaTime);
        }

        private void Rotate()
        {
            float lerpAngle = Mathf.LerpAngle(Character.Move.Angle, _angle, 0.25f);
            Character.Move.transform.rotation = Quaternion.Euler(0f, lerpAngle, 0f);
        }
    }
}