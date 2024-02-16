using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

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
        
        async UniTask<RenderTexture> ITextureArrayFactory.GetRenderTexture()
        {
            PreviewData data = _staticDataService.PreviewData();
            
            return await _assetService.LoadFromAddressable<RenderTexture>(data.AssetReferenceRenderTexture);
        }

        int ITextureArrayFactory.GetIndex() => _index;

        void ITextureArrayFactory.CreateTextureArray()
        {
            Texture2D[] textures = _staticDataService.TextureArrayData().Textures;
            
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
                _textureArray.SetPixels(_staticDataService.TextureArrayData().Textures[i].GetPixels(0), i, 0);
            }
            
            _textureArray.Apply();
        }

        void ITextureArrayFactory.GenerateRandomTextureIndex() => _index = _staticDataService.TextureArrayData().Textures.GetRandomIndex();

        void ITextureArrayFactory.CleanUp()
        {
            _textureArray = null;
            _index = -1;
        }
    }
}