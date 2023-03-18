using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyMeleeAttack : SystemComponent<CMelee>
    {
        private IGameFactory _gameFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CMelee component)
        {
            base.OnEnableComponent(component);

            component.OnAttack
                .Subscribe(_ =>
                {
                    float distance = Vector3.Distance(component.transform.position, _gameFactory.CurrentCharacter.Position);

                    if (distance > 1.5f)
                    {
                        return;
                    }

                    _gameFactory.CurrentCharacter.Health.Hit.Execute(component.Damage);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CMelee component)
        {
            base.OnDisableComponent(component);
        }
    }
}