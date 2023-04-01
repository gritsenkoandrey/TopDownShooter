using CodeBase.ECSCore;
using CodeBase.Game.Components;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletLifeTime : SystemComponent<CBullet>
    {
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

            component.Tween = DOVirtual.DelayedCall(2.5f, () => Object.Destroy(component.Object));
        }

        protected override void OnDisableComponent(CBullet component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }
    }
}