using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "TextureArrayData", menuName = "Data/TextureArrayData", order = 0)]
    public sealed class TextureArrayData : ScriptableObject
    {
        public Texture2D[] Textures;
    }
}