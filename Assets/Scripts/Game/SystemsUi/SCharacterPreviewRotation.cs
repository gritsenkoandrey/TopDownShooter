using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewRotation : SystemComponent<CCharacterPreviewRotation>
    {
        protected override void OnEnableComponent(CCharacterPreviewRotation component)
        {
            base.OnEnableComponent(component);

            component.OnTouch
                .Subscribe(eventData =>
                {
                    Vector3 delta = new Vector3(0f, -eventData.delta.x, 0f);

                    component.Model.localEulerAngles += delta;
                })
                .AddTo(component.LifetimeDisposable);

            component.OnStartTouch
                .Subscribe(_ =>
                {
                    component.Tween?.Kill();
                })
                .AddTo(component.LifetimeDisposable);
            
            component.OnEndTouch
                .Subscribe(_ =>
                {
                    component.Tween = component.Model.DOLocalRotateQuaternion(Quaternion.identity, 1f);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacterPreviewRotation component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }
    }
}