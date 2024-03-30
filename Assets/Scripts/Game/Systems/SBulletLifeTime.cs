using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Pool;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletLifeTime : SystemComponent<CBullet>
    {
        private IObjectPoolService _objectPoolService;

        [Inject]
        private void Construct(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            Entities.Foreach(ImmediateReturnToPool);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(DestroyBulletAfterTime);
        }

        protected override void OnEnableComponent(CBullet component)
        {
            base.OnEnableComponent(component);

            component.OnDestroy
                .First()
                .Subscribe(_ => ReturnToPool(component).Forget())
                .AddTo(component.LifetimeDisposable);
        }

        private void DestroyBulletAfterTime(CBullet bullet)
        {
            bullet.LifeTime -= Time.deltaTime;

            if (bullet.LifeTime < 0f)
            {
                ReturnToPool(bullet).Forget();
            }
        }

        private async UniTaskVoid ReturnToPool(IObject component)
        {
            await UniTask.Yield();
            
            _objectPoolService.ReleaseObject(component.Object);
        }

        private void ImmediateReturnToPool(CBullet component)
        {
            _objectPoolService.ReleaseObject(component.Object);
        }
    }
}