using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Pool;
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
        private readonly IObjectPoolService _objectPoolService;

        public SZombieCollision(IGameFactory gameFactory, IObjectPoolService objectPoolService)
        {
            _gameFactory = gameFactory;
            _objectPoolService = objectPoolService;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
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

                        GameObject prefab = _gameFactory.CreateHitFx(bullet.Object.transform.position);

                        DOVirtual.DelayedCall(1f, () => _objectPoolService.ReleaseObject(prefab));

                        bullet.Rigidbody.isKinematic = true;
                        _objectPoolService.ReleaseObject(bullet.Object);
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