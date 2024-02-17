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

        RenderTexture ITextureArrayFactory.CreateRenderTexture()
        {
            RenderTextureSettings data = _staticDataService.TextureArrayData().RenderTextureSettings;
            RenderTexture renderTexture = new RenderTexture(data.Resolution.x, data.Resolution.y, 
                data.ColorFormat, data.DepthStensilFormat);
            renderTexture.Create();
            return renderTexture;
        }

        async UniTask ITextureArrayFactory.CreateTextureArray()
        {
            TextureData data = _staticDataService.TextureArrayData();
            Texture2D[] textures = new Texture2D[data.Textures.Length];
            
            for (int i = 0; i < data.Textures.Length; i++)
            {
                textures[i] = await _assetService.LoadFromAddressable<Texture2D>(data.Textures[i]);
            }
            
            int width = data.TextureArraySettings.Resolution.x;
            int height = data.TextureArraySettings.Resolution.y;
            int length = textures.Length;
            TextureFormat format = data.TextureArraySettings.TextureFormat;
            bool mipChain = data.TextureArraySettings.IsMipChain;
            bool liner = data.TextureArraySettings.IsLiner;

            _textureArray = new Texture2DArray(width, height, length, format, mipChain, liner)
            {
                filterMode = data.TextureArraySettings.FilterMode,
                wrapMode = data.TextureArraySettings.WrapMode,
            };

            for (int i = 0; i < length; i++)
            {
                _textureArray.SetPixels(textures[i].GetPixels(0), i, 0);
            }
            
            _textureArray.Apply();
        }

        Texture2DArray ITextureArrayFactory.GetTextureArray() => _textureArray;
        int ITextureArrayFactory.GetIndex() => _index;
        void ITextureArrayFactory.GenerateRandomTextureIndex() => 
            _index = _staticDataService.TextureArrayData().Textures.GetRandomIndex();

        void ITextureArrayFactory.CleanUp()
        {
            _textureArray = null;
            _index = -1;
        }
    }
}