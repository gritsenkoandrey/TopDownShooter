﻿using System;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.GUI;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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

        void IDisposable.Dispose()
        {
            RenderTexture.Release();
            RenderTexture = null;
        }
    }
}