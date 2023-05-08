using UnityEngine;

namespace CodeBase.Utils
{
    public static class Shaders
    {
        public static int TextureArray => Shader.PropertyToID("_TextureArray");
        public static int Index => Shader.PropertyToID("_Index");
    }
}