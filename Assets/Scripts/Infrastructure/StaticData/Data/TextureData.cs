using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Experimental.Rendering;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(TextureData), menuName = "Data/" + nameof(TextureData))]
    public sealed class TextureData : ScriptableObject
    {
        public AssetReferenceTexture2D[] Textures;
        public RenderTextureSettings RenderTextureSettings;
        public TextureArraySettings TextureArraySettings;
    }

    [System.Serializable]
    public struct RenderTextureSettings
    {
        public Vector2Int Resolution;
        public GraphicsFormat ColorFormat;
        public GraphicsFormat DepthStensilFormat;
    }
    
    [System.Serializable]
    public struct TextureArraySettings
    {
        public Vector2Int Resolution;
        public TextureFormat TextureFormat;
        public FilterMode FilterMode;
        public TextureWrapMode WrapMode;
        public bool IsMipChain;
        public bool IsLiner;
    }
}