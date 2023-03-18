using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.LevelData;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IAsset _asset;
        private readonly IStaticDataService _staticDataService;

        public Level CurrentLevel { get; private set; }
        public ICharacter CurrentCharacter { get; private set; }

        public GameFactory(IAsset asset, IStaticDataService staticDataService)
        {
            _asset = asset;
            _staticDataService = staticDataService;
        }
        
        public Level CreateLevel()
        {
            return CurrentLevel = Object.Instantiate(_asset.GameAssetData.Level);
        }

        public ICharacter CreateCharacter()
        {
            CharacterData characterData = _staticDataService.CharacterData();
            
            return CurrentCharacter = Object.Instantiate(characterData.Prefab, _asset.GameAssetData.Level.CharacterSpawnPosition, Quaternion.identity);
        }

        public IBullet CreateBullet(Vector3 position)
        {
            return Object.Instantiate(_asset.GameAssetData.Bullet, position, Quaternion.identity);
        }

        public CEnemy CreateZombie(ZombieType zombieType, Vector3 position, Transform parent)
        {
            ZombieData data = _staticDataService.ZombieData(zombieType);
            
            CEnemy zombie = Object.Instantiate(data.Prefab, position, Quaternion.identity, parent);

            zombie.Health.Health = data.Health;
            zombie.Melee.Damage = data.Damage;
            
            CurrentCharacter.Enemies.Add(zombie);

            return zombie;
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