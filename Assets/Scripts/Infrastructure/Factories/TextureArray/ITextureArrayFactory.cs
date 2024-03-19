using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.TextureArray
{
    public interface ITextureArrayFactory
    {
        RenderTexture CreateRenderTexture();
        UniTask CreateTextureArray();
        Texture2DArray GetTextureArray();
        int GetIndex();
        void GenerateRandomTextureIndex();
        void CleanUp();
    }
}