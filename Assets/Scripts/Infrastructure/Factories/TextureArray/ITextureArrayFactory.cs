using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.TextureArray
{
    public interface ITextureArrayFactory
    {
        public RenderTexture CreateRenderTexture();
        public UniTask CreateTextureArray();
        public Texture2DArray GetTextureArray();
        public int GetIndex();
        public void GenerateRandomTextureIndex();
        public void CleanUp();
    }
}