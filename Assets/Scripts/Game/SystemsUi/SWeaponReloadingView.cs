using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SWeaponReloadingView : SystemComponent<CWeaponReloadingView>
    {
        private ICameraService _cameraService;
        private InventoryModel _inventoryModel;
        private LevelModel _levelModel;

        [Inject]
        private void Construct(ICameraService cameraService, InventoryModel inventoryModel, LevelModel levelModel)
        {
            _cameraService = cameraService;
            _inventoryModel = inventoryModel;
            _levelModel = levelModel;
        }

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Entities.Foreach(UpdatePosition);
        }

        protected override void OnEnableComponent(CWeaponReloadingView component)
        {
            base.OnEnableComponent(component);

            SetAlpha(component, 0f);
            SubscribeOnReloadingClip(component);
        }

        protected override void OnDisableComponent(CWeaponReloadingView component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private void SetAlpha(CWeaponReloadingView component, float alpha) => component.CanvasGroup.alpha = alpha;

        private void SubscribeOnReloadingClip(CWeaponReloadingView component)
        {
            _inventoryModel.Reloading
                .Subscribe(delay =>
                {
                    SetAlpha(component, 1f);

                    component.Tween = DOVirtual.Float(0f, 1f, delay, value => component.Fill.fillAmount = value)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => SetAlpha(component, 0f));
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void UpdatePosition(CWeaponReloadingView component)
        {
            Vector3 targetPosition = _levelModel.Character.Position.AddY(_levelModel.Character.Height);
            Vector3 targetScreenPosition = _cameraService.Camera.WorldToScreenPoint(targetPosition);
            
            component.Transform.position = targetScreenPosition;
        }
    }
}