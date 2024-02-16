using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.GUI;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class CharacterPreviewModel : IInitializable
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGuiService _guiService;
        private readonly ITextureArrayFactory _textureArrayFactory;
        
        public CCharacterPreview CharacterPreview { get; private set; }
        
        public CharacterPreviewModel(IGameFactory gameFactory, IGuiService guiService, ITextureArrayFactory textureArrayFactory)
        {
            _gameFactory = gameFactory;
            _guiService = guiService;
            _textureArrayFactory = textureArrayFactory;
        }
        
        public void Initialize()
        {
            InitCharacterPreview().Forget();
        }

        private async UniTaskVoid InitCharacterPreview()
        {
            CharacterPreview = await _gameFactory.CreateCharacterPreview();
            
            CharacterPreview.Camera.targetTexture = await _textureArrayFactory.GetRenderTexture();
            CharacterPreview.Camera.orthographicSize *= _guiService.ScaleFactor;
            CharacterPreview.Camera.enabled = true;
        }
    }
}