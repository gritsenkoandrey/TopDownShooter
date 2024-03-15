using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SToggleController : SystemComponent<CToggle>
    {
        protected override void OnEnableComponent(CToggle component)
        {
            base.OnEnableComponent(component);

            component.Handle.OnDrag
                .Subscribe(data => HandleDrag(component, data.delta))
                .AddTo(component.LifetimeDisposable);

            component.Handle.OnEndDrag
                .Subscribe(_ => component.IsEnable.Value = component.Handle.X > 0)
                .AddTo(component.LifetimeDisposable);

            component.OnClick
                .Subscribe(_ => component.IsEnable.Value = !component.IsEnable.Value)
                .AddTo(component.LifetimeDisposable);

            component.IsEnable
                .Subscribe(isEnable => SetActive(component, isEnable))
                .AddTo(component.LifetimeDisposable);
        }

        private void SetActive(CToggle component, bool isActive)
        {
            float posX = component.Offset * (isActive ? 1 : -1);
            
            component.Tween?.Kill();
            
            component.Tween = component.Handle.transform
                .DOLocalMoveX(posX, 0.1f)
                .SetEase(Ease.Linear)
                .SetLink(component.gameObject);
            
            UpdateVisual(component, isActive);
        }

        private void HandleDrag(CToggle component, Vector2 delta)
        {
            Vector3 initPos = component.Handle.transform.localPosition;
            float xPos = Mathf.Clamp(initPos.x + delta.x, -component.Offset, component.Offset);
            component.Handle.transform.localPosition = new Vector3(xPos, initPos.y, initPos.z);
            
            UpdateVisual(component, component.Handle.X > 0);
        }

        private void UpdateVisual(CToggle component, bool isActive)
        {
            component.IsActive(isActive);
            component.Handle.IsActive(isActive);
        }
    }
}