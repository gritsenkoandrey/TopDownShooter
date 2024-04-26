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
    public sealed class SScreenVisualReloadingWeapon : SystemComponent<CWeaponReloadingView>
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

            SubscribeOnReloadingClip(component);
        }

        private void SubscribeOnReloadingClip(CWeaponReloadingView component)
        {
            void SetFillAmount(float value) => component.Fill.fillAmount = value;
            void SetCanvasGroupAlphaOne() => component.CanvasGroup.alpha = 1f;
            void SetCanvasGroupAlphaZero() => component.CanvasGroup.alpha = 0f;

            _inventoryModel.ReloadingWeapon
                .DoOnSubscribe(SetCanvasGroupAlphaZero)
                .Subscribe(delay =>
                {
                    DOVirtual.Float(0f, 1f, delay, SetFillAmount)
                        .SetEase(Ease.Linear)
                        .SetLink(component.gameObject)
                        .OnStart(SetCanvasGroupAlphaOne)
                        .OnComplete(SetCanvasGroupAlphaZero);
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