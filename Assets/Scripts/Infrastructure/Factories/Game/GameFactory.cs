using CodeBase.Infrastructure.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;

        public GameObject CurrentLevel { get; private set; }

        public GameFactory(IAsset asset)
        {
            _asset = asset;
        }
        
        public GameObject CreateLevel()
        {
            return CurrentLevel = Object.Instantiate(_asset.GameAssetData.Level);
        }
        
        public GameObject CreateCanvas()
        {
            return Object.Instantiate(_asset.UiAssetData.Canvas);
        }
    }
}