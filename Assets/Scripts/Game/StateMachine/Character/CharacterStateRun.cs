using CodeBase.Game.Components;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateRun : CharacterState, IState
    {
        private readonly IJoystickService _joystickService;
        private readonly ICameraService _cameraService;
        
        private float _angle;

        public CharacterStateRun(IStateMachine stateMachine, CCharacter character, IJoystickService joystickService, ICameraService cameraService) 
            : base(stateMachine, character)
        {
            _joystickService = joystickService;
            _cameraService = cameraService;
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
            return _joystickService.GetAxis().sqrMagnitude < 0.1f;
        }

        private void Move()
        {
            _angle = Mathf.Atan2(_joystickService.GetAxis().x, _joystickService.GetAxis().y) * 
                Mathf.Rad2Deg + _cameraService.Camera.transform.eulerAngles.y; 

            Vector3 move = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;

            Vector3 next = Character.Position + move * Character.Move.Speed * Time.deltaTime;
                        
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