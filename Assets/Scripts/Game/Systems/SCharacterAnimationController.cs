using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterAnimationController : SystemComponent<CCharacter>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();

            foreach (CCharacter animator in Entities)
            {
                UpdateAnimation(animator);
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);
            
            component.Attack.Attack
                .Subscribe(_ =>
                {
                    component.Animator.SetTrigger(Animations.Shoot);
                    component.Animator.SetFloat(Animations.ShootBlend, Random.Range(0, 2));
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }

        private void UpdateAnimation(CCharacter character)
        {
            character.Animator.SetFloat(Animations.Velocity, character.CharacterController.velocity.sqrMagnitude, 0.05f, Time.deltaTime);
        }
    }
}