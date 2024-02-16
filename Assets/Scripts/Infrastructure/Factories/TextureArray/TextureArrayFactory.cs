using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace CodeBase.Infrastructure.Factories.TextureArray
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class TextureArrayFactory : ITextureArrayFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetService _assetService;

        private Texture2DArray _textureArray;
        private int _index;

        public TextureArrayFactory(IStaticDataService staticDataService, IAssetService assetService)
        {
            _staticDataService = staticDataService;
            _assetService = assetService;
        }

        Texture2DArray ITextureArrayFactory.GetTextureArray() => _textureArray;
        
        RenderTexture ITextureArrayFactory.CreateRenderTexture()
        {
            RenderTexture renderTexture = new RenderTexture(512, 512, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.D16_UNorm);
            renderTexture.Create();
            return renderTexture;
        }

        int ITextureArrayFactory.GetIndex() => _index;

        async UniTask ITextureArrayFactory.CreateTextureArray()
        {
            TextureData data = _staticDataService.TextureArrayData();
            Texture2D[] textures = new Texture2D[data.Textures.Length];
            
            for (int i = 0; i < data.Textures.Length; i++)
            {
                textures[i] = await _assetService.LoadFromAddressable<Texture2D>(data.Textures[i]);
            }
            
            int width = textures[0].width;
            int height = textures[0].height;
            int length = textures.Length;

            _textureArray = new Texture2DArray(width, height, length, TextureFormat.RGBA32, true, false)
            {
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Repeat
            };

            for (int i = 0; i < length; i++)
            {
                _textureArray.SetPixels(textures[i].GetPixels(0), i, 0);
            }
            
            _textureArray.Apply();
            await UniTask.Yield();
        }

        void ITextureArrayFactory.GenerateRandomTextureIndex() => _index = _staticDataService.TextureArrayData().Textures.GetRandomIndex();

        void ITextureArrayFactory.CleanUp()
        {
            _textureArray = null;
            _index = -1;
        }
    }
}