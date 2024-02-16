using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(TextureData), menuName = "Data/" + nameof(TextureData))]
    public sealed class TextureData : ScriptableObject
    {
        public Texture2D[] Textures;
    }
}