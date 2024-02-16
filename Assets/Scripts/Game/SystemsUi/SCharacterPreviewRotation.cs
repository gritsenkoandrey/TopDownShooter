using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewRotation : SystemComponent<CCharacterPreviewRotation>
    {
        private CharacterPreviewModel _characterPreviewModel;
        private ITextureArrayFactory _textureArrayFactory;
        
        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel, ITextureArrayFactory textureArrayFactory)
        {
            _characterPreviewModel = characterPreviewModel;
            _textureArrayFactory = textureArrayFactory;
        }
        
        protected override void OnEnableComponent(CCharacterPreviewRotation component)
        {
            base.OnEnableComponent(component);
            
            SetRenderTexture(component).Forget();

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

        protected override void OnDisableComponent(CCharacterPreviewRotation component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private async UniTaskVoid SetRenderTexture(CCharacterPreviewRotation component)
        {
            RenderTexture renderTexture = await _textureArrayFactory.GetRenderTexture();

            component.RawImage.texture = renderTexture;
            component.RawImage.enabled = true;
        }
    }
}