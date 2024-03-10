using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopCharacterRenderer : SystemComponent<CShopCharacterRenderer>
    {
        private CharacterPreviewModel _characterPreviewModel;
        
        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel)
        {
            _characterPreviewModel = characterPreviewModel;
        }
        
        protected override void OnEnableComponent(CShopCharacterRenderer component)
        {
            base.OnEnableComponent(component);
            
            SetRenderTexture(component);

            component.OnTouch
                .Subscribe(eventData =>
                {
                    Vector3 delta = new Vector3(0f, -eventData.delta.x, 0f);
            
                    _characterPreviewModel.CharacterPreview.CharacterPreviewModel.transform.localEulerAngles += delta;
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
                    component.Tween = _characterPreviewModel.CharacterPreview.CharacterPreviewModel.transform
                        .DOLocalRotateQuaternion(Quaternion.identity, 1f);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CShopCharacterRenderer component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private void SetRenderTexture(CShopCharacterRenderer component)
        {
            component.RawImage.texture = _characterPreviewModel.RenderTexture;
            component.RawImage.enabled = true;
        }
    }
}