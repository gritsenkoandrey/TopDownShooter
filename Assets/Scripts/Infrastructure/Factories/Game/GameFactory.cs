using CodeBase.Game.Builders.Levels;
using CodeBase.Game.Builders.Player;
using CodeBase.Game.Components;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressService _progressService;
        private readonly IAssetService _assetService;
        private readonly LevelModel _levelModel;

        public GameFactory(
            IStaticDataService staticDataService, 
            IProgressService progressService, 
            IAssetService assetService,
            LevelModel levelModel)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _assetService = assetService;
            _levelModel = levelModel;
        }

        async UniTask<ILevel> IGameFactory.CreateLevel()
        {
            Level data = GetLevel(_staticDataService.LevelData());

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            ILevel level = new LevelBuilder()
                .SetPrefab(prefab)
                .SetData(data)
                .Build();
            
            _levelModel.SetLevel(level);
            
            return level;
        }

        async UniTask<CCharacter> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
        {
            CharacterData data = _staticDataService.CharacterData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CCharacter character = new CharacterBuilder()
                .SetPrefab(prefab)
                .SetData(data)
                .SetParent(parent)
                .SetPosition(position)
                .Build();
            
            _levelModel.SetCharacter(character);

            return character;
        }
        
        async UniTask<CUnit> IGameFactory.CreateUnit(Vector3 position, Transform parent)
        {
            UnitData data = _staticDataService.UnitData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefabreference);

            CUnit unit = new UnitBuilder()
                .SetPrefab(prefab)
                .SetParent(parent)
                .SetPosition(position)
                .Build();
            
            _levelModel.AddEnemy(unit);

            return unit;
        }

        async UniTask<CTurret> IGameFactory.CreateTurret(TurretType turretType, Vector3 position, Transform parent)
        {
            TurretData data = _staticDataService.TurretData(turretType);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CTurret turret = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CTurret>();
            _levelModel.AddEnemy(turret);
            return turret;
        }

        async UniTask<CCharacterPreview> IGameFactory.CreateCharacterPreview()
        {
            PreviewData data = _staticDataService.PreviewData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            return Object.Instantiate(prefab).GetComponent<CCharacterPreview>();
        }

        private Level GetLevel(LevelData data)
        {
            int index;
            int level = _progressService.LevelData.Data.Value;

            if (level > data.Levels.Length)
            {
                index = (level - 1) % data.Levels.Length;
            }
            else
            {
                index = level - 1;
            }
            
            return data.Levels[index];
        }
    }
}