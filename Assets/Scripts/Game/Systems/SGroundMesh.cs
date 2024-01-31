using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Utils;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SGroundMesh : SystemComponent<CMesh>
    {
        private ITextureArrayFactory _textureArrayFactory;

        [Inject]
        public void Construct(ITextureArrayFactory textureArrayFactory)
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