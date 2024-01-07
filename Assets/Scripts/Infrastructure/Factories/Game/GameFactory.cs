using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressService _progressService;
        private readonly ICameraService _cameraService;
        private readonly IAssetService _assetService;
        private readonly LevelModel _levelModel;

        public GameFactory(
            IStaticDataService staticDataService, 
            IProgressService progressService, 
            ICameraService cameraService, 
            IAssetService assetService,
            LevelModel levelModel)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _cameraService = cameraService;
            _assetService = assetService;
            _levelModel = levelModel;
        }

        async UniTask<ILevel> IGameFactory.CreateLevel()
        {
            LevelData data = _staticDataService.LevelData(GetLevelType());

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CLevel level = new LevelBuilder()
                .Reset()
                .SetPrefab(prefab.GetComponent<CLevel>())
                .SetLevelType(data.LevelType)
                .SetLevelTime(data.LevelTime)
                .Build();
            
            _levelModel.SetLevel(level);
            
            return level;
        }

        async UniTask<ICharacter> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
        {
            CharacterData data = _staticDataService.CharacterData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CCharacter character = new CharacterBuilder()
                .Reset()
                .SetPrefab(prefab.GetComponent<CCharacter>())
                .SetParent(parent)
                .SetCamera(_cameraService)
                .SetPosition(position)
                .SetHealth(data.Health)
                .SetSpeed(data.Speed)
                .Build();
            
            _levelModel.SetCharacter(character);

            return character;
        }

        async UniTask<CZombie> IGameFactory.CreateZombie(ZombieType zombieType, Vector3 position, Transform parent)
        {
            ZombieData data = _staticDataService.ZombieData(zombieType);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CZombie zombie = new ZombieBuilder()
                .Reset()
                .SetPrefab(prefab.GetComponent<CZombie>())
                .SetPosition(position)
                .SetParent(parent)
                .SetHealth(data.Health)
                .SetDamage(data.Damage)
                .SetStats(data.Stats)
                .Build();
            
            _levelModel.AddEnemy(zombie);

            return zombie;
        }

        void IGameFactory.CleanUp()
        {
        }

        private LevelType GetLevelType() => _progressService.LevelData.Data.Value % 5 == 0 ? LevelType.Boss : LevelType.Normal;
    }
}