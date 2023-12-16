using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Utils;

namespace CodeBase.Game.Systems
{
    public sealed class SGroundMesh : SystemComponent<CMesh>
    {
        private readonly ITextureArrayFactory _textureArrayFactory;

        public SGroundMesh(ITextureArrayFactory textureArrayFactory)
        {
            _textureArrayFactory = textureArrayFactory;
        }

        protected override void OnEnableComponent(CMesh component)
        {
            base.OnEnableComponent(component);
            
            component.Renderer.material.SetTexture(Shaders.TextureArray, _textureArrayFactory.GetTextureArray());
            component.Renderer.material.SetInt(Shaders.Index, _textureArrayFactory.GetIndex());
        }
    }
}