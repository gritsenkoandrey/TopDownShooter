using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(TextureArrayData), menuName = "Data/" + nameof(TextureArrayData))]
    public sealed class TextureArrayData : ScriptableObject
    {
        public Texture2D[] Textures;
    }
}