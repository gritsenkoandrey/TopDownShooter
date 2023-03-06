using AndreyGritsenko.ECSCore;
using AndreyGritsenko.Game.Components;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AndreyGritsenko.Game.Systems
{
    public sealed class SEnemyMovement : SystemComponent<CEnemy>
    {
        private readonly CCharacter _character;

        public SEnemyMovement(CCharacter character)
        {
            _character = character;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            component.Agent
                .UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Vector3.Distance(component.Agent.transform.position, _character.transform.position) >
                        component.Agent.stoppingDistance)
                    {
                        component.Agent.SetDestination(_character.transform.position);
                    }
                    else
                    {
                        if (component.Agent.hasPath)
                        {
                            component.Agent.ResetPath();
                        }
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}