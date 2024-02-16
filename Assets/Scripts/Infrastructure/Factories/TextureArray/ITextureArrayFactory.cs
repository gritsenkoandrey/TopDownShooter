using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.TextureArray
{
    public interface ITextureArrayFactory
    {
        public Texture2DArray GetTextureArray();
        public UniTask<RenderTexture> GetRenderTexture();
        public int GetIndex();
        public void CreateTextureArray();
        public void GenerateRandomTextureIndex();
        public void CleanUp();
    }
}