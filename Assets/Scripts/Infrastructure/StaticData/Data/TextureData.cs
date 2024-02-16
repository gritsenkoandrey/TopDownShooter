using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(TextureData), menuName = "Data/" + nameof(TextureData))]
    public sealed class TextureData : ScriptableObject
    {
        public AssetReferenceTexture2D[] Textures;
    }
}