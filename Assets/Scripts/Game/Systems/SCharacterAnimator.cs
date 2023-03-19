using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterAnimator : SystemComponent<CCharacter>
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

            foreach (CCharacter character in Entities)
            {
                character.Animator.UpdateAnimator.Execute(character.CharacterController.velocity.sqrMagnitude);
            }
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);
            
            component.Weapon.Shoot
                .Subscribe(_ =>
                {
                    component.Animator.Animator.SetTrigger(Animations.Shoot);
                    component.Animator.Animator.SetFloat(Animations.ShootBlend, Random.Range(0, 2));
                })
                .AddTo(component.LifetimeDisposable);

            component.Animator.UpdateAnimator
                .Subscribe(velocity =>
                {
                    component.Animator.Animator.SetFloat(Animations.Velocity, velocity, 0.05f, Time.deltaTime);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}