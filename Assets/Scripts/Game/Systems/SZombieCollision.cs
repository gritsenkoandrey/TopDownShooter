using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieCollision : SystemComponent<CZombie>
    {
        private readonly IGameFactory _gameFactory;

        public SZombieCollision(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
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
        }

        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.Health.Collider
                .OnTriggerEnterAsObservable()
                .Where(collider => collider.gameObject.layer.Equals(Layers.Bullet))
                .Subscribe(collider =>
                {
                    if (collider.TryGetComponent(out IBullet bullet))
                    {
                        component.Health.Health.Value -= bullet.Damage;

                        component.IsAggro = true;

                        Transform prefab = _gameFactory.CreateHitFx(bullet.Object.transform.position);

                        DOVirtual.DelayedCall(1f, () => Object.Destroy(prefab.gameObject));
                        
                        Object.Destroy(bullet.Object);
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CZombie component)
        {
            base.OnDisableComponent(component);
        }
    }
}