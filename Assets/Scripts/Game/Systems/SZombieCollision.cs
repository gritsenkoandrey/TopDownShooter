using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Utils;
using UniRx;
using UniRx.Triggers;

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

                        _gameFactory.CreateHitFx(bullet.Object.transform.position);

                        bullet.OnDestroy.Execute();
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