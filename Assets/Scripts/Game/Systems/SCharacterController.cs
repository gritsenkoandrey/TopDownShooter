using AndreyGritsenko.ECSCore;
using AndreyGritsenko.Game.Components;
using AndreyGritsenko.Utils;
using SimpleInputNamespace;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AndreyGritsenko.Game.Systems
{
    public sealed class SCharacterController : SystemComponent<CCharacter>
    {
        private readonly Joystick _joystick;
        
        public SCharacterController(Joystick joystick)
        {
            _joystick = joystick;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            Camera camera = Camera.main;
            float currentVelocity = default;
            float gravity = Physics.gravity.y * 10f;
            float speed = 10f;
            
            component.CharacterController
                .UpdateAsObservable()
                .Subscribe(_ =>
                {
                    Vector3 move = Vector3.zero;

                    if (_joystick.Value.magnitude > 0.1f)
                    {
                        float angle = Mathf.Atan2(_joystick.Value.x, _joystick.Value.y) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
                        float smoothAngle = Mathf.SmoothDampAngle(component.transform.eulerAngles.y, angle, ref currentVelocity, 0.05f);
                        
                        component.transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                        move = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

                        Vector3 next = component.transform.position + move * speed * Time.deltaTime;
                        
                        Ray ray = new Ray { origin = next, direction = Vector3.down };
                        
                        if (!Physics.Raycast(ray, 5f, Layers.Ground)) return;
                    }

                    move.y = component.CharacterController.isGrounded ? 0f : gravity;

                    component.CharacterController.Move(move * speed * Time.deltaTime);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}