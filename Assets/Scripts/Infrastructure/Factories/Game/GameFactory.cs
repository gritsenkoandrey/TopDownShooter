using CodeBase.Game.Interfaces;
using CodeBase.Game.LevelData;
using CodeBase.Infrastructure.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;

        public Level CurrentLevel { get; private set; }
        public ICharacter CurrentCharacter { get; private set; }

        public GameFactory(IAsset asset)
        {
            _asset = asset;
        }
        
        public Level CreateLevel()
        {
            return CurrentLevel = Object.Instantiate(_asset.GameAssetData.Level);
        }

        public ICharacter CreateCharacter()
        {
            return CurrentCharacter = Object.Instantiate(_asset.GameAssetData.Character, _asset.GameAssetData.Level.CharacterSpawnPosition, Quaternion.identity);
        }

        public void CleanUp()
        {
            if (CurrentCharacter != null)
            {
                Object.Destroy(CurrentCharacter.Object);
            }
            
            if (CurrentLevel != null)
            {
                Object.Destroy(CurrentLevel.gameObject);
            }

            CurrentLevel = null;
            CurrentCharacter = null;
        }
    }
}