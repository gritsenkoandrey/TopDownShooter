using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyMeleeAttack : SystemComponent<CEnemy>
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

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            component.Melee.OnAttack
                .Subscribe(_ =>
                {
                    float distance = Vector3.Distance(component.transform.position, _gameFactory.CurrentCharacter.Position);

                    if (distance > 1.5f || component.Health.Health.Value <= 0)
                    {
                        return;
                    }

                    _gameFactory.CurrentCharacter.Health.Health.Value -= component.Melee.Damage;
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}