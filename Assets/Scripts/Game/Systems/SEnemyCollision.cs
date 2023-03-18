using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Utils;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyCollision : SystemComponent<CEnemy>
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
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            component.Health.Collider
                .OnTriggerEnterAsObservable()
                .Where(collider => collider.gameObject.layer.Equals(Layers.Bullet))
                .Subscribe(collider =>
                {
                    if (collider.TryGetComponent(out IBullet bullet))
                    {
                        component.Health.Hit.Execute(bullet.Damage);
                        
                        Object.Destroy(bullet.Object);
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