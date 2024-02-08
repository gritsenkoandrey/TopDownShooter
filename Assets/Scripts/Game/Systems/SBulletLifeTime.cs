using System;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Pool;
using Cysharp.Threading.Tasks;
using UniRx;
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

        protected override void OnEnableComponent(CBullet component)
        {
            base.OnEnableComponent(component);

            component.OnDestroy
                .First()
                .Subscribe(_ => ReturnToPool(component).Forget())
                .AddTo(component.LifetimeDisposable);

            Observable.Timer(Time(component.LifeTime))
                .First()
                .Subscribe(_ => ReturnToPool(component).Forget())
                .AddTo(component.LifetimeDisposable);
        }

        private async UniTaskVoid ReturnToPool(IObject component)
        {
            await UniTask.Yield();

            _objectPoolService.ReleaseObject(component.Object);
        }

        private TimeSpan Time(float lifeTime) => TimeSpan.FromSeconds(lifeTime);
    }
}