using CodeBase.Game.Components;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Input;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Character
{
    public sealed class CharacterStateRun : CharacterState, IState
    {
        private IJoystickService _joystickService;
        private ICameraService _cameraService;

        private const float RayDistance = 5f;
        private const float LerpRotate = 0.25f;
        
        private float _angle;

        public CharacterStateRun(IStateMachine stateMachine, CCharacter character) : base(stateMachine, character)
        {
        }
        
        [Inject]
        private void Construct(IJoystickService joystickService, ICameraService cameraService)
        {
            _joystickService = joystickService;
            _cameraService = cameraService;
        }

        void IState.Enter()
        {
            Character.Animator.OnRun.Execute(1f);
            Character.Radar.Draw.Execute();
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
            return _joystickService.GetAxis().sqrMagnitude < MinInputMagnitude;
        }

        private void Move()
        {
            _angle = Mathf.Atan2(_joystickService.GetAxis().x, _joystickService.GetAxis().y) * 
                Mathf.Rad2Deg + _cameraService.Camera.transform.eulerAngles.y; 

            Vector3 move = Quaternion.Euler(0f, _angle, 0f) * Vector3.forward;

            Vector3 next = Character.Position + move * Character.CharacterController.Speed * Time.deltaTime;
                        
            Ray ray = new Ray { origin = next, direction = Vector3.down };

            if (!Physics.Raycast(ray, RayDistance, Layers.Ground))
            {
                return;
            }
                
            move.y = Character.CharacterController.IsGrounded ? 0f : Physics.gravity.y;

            Character.CharacterController.CharacterController.Move(move * Character.CharacterController.Speed * Time.deltaTime);
        }

        private void Rotate()
        {
            float lerpAngle = Mathf.LerpAngle(Character.CharacterController.Angle, _angle, LerpRotate);
            Character.CharacterController.transform.rotation = Quaternion.Euler(0f, lerpAngle, 0f);
        }
    }
}