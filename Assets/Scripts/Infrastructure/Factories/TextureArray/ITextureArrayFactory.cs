using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.TextureArray
{
    public interface ITextureArrayFactory
    {
        public Texture2DArray GetTextureArray();
        public RenderTexture CreateRenderTexture();
        public int GetIndex();
        public UniTask CreateTextureArray();
        public void GenerateRandomTextureIndex();
        public void CleanUp();
    }
}