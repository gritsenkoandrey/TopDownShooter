using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Pool;
using DG.Tweening;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletLifeTime : SystemComponent<CBullet>
    {
        private readonly IObjectPoolService _objectPoolService;

        public SBulletLifeTime(IObjectPoolService objectPoolService)
        {
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

        protected override void OnEnableComponent(CBullet component)
        {
            base.OnEnableComponent(component);

            component.Rigidbody.isKinematic = false;

            component.OnDestroy
                .Subscribe(_ =>
                {
                    _objectPoolService.ReleaseObject(component.Object);
                })
                .AddTo(component.LifetimeDisposable);

            component.Tween = DOVirtual.DelayedCall(2f, () =>
            {
                _objectPoolService.ReleaseObject(component.Object);
            });
        }

        protected override void OnDisableComponent(CBullet component)
        {
            base.OnDisableComponent(component);
            
            component.Rigidbody.isKinematic = true;
            
            component.Tween?.Kill();
        }
    }
}