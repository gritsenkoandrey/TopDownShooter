using System;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.GUI;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CharacterPreviewModel : IInitializable, IDisposable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGuiService _guiService;
        private readonly ITextureArrayFactory _textureArrayFactory;
        
        public CCharacterPreview CharacterPreview { get; private set; }
        public RenderTexture RenderTexture { get; private set; }

        public readonly IReactiveProperty<PreviewState> State = 
            new ReactiveProperty<PreviewState>(PreviewState.Start);

        public CharacterPreviewModel(IGameFactory gameFactory, IGuiService guiService, ITextureArrayFactory textureArrayFactory)
        {
            _gameFactory = gameFactory;
            _guiService = guiService;
            _textureArrayFactory = textureArrayFactory;
        }
        
        void IInitializable.Initialize()
        {
            InitCharacterPreview().Forget();
        }

        private async UniTaskVoid InitCharacterPreview()
        {
            RenderTexture = _textureArrayFactory.CreateRenderTexture();
            CharacterPreview = await _gameFactory.CreateCharacterPreview();
            CharacterPreview.Camera.targetTexture = RenderTexture;
            CharacterPreview.Camera.orthographicSize *= _guiService.ScaleFactor;
            CharacterPreview.Camera.enabled = true;
        }

        public void PlayAnimation(int animation) => 
            CharacterPreview.CharacterPreviewAnimator.Animator.CrossFade(animation, default);

        public void PlayPreviewAnimation()
        {
            CharacterPreview.CharacterPreviewAnimator.Animator
                .SetFloat(Animations.PreviewBlend, UnityEngine.Random.Range(0, 4));
            PlayAnimation(Animations.Preview);
        }

        void IDisposable.Dispose()
        {
            RenderTexture.Release();
            RenderTexture = null;
        }
    }
}