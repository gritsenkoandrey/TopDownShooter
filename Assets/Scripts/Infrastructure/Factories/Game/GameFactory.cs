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

        public GameFactory(IStaticDataService staticDataService, IProgressService progressService, 
            IAssetService assetService, LevelModel levelModel)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _assetService = assetService;
            _levelModel = levelModel;
        }

        async UniTask<ILevel> IGameFactory.CreateLevel()
        {
            LevelData levelData = _staticDataService.LevelData();
            int curLevel = _progressService.LevelData.Data.Value;
            int index = curLevel > levelData.Levels.Length ? (curLevel - 1) % levelData.Levels.Length : curLevel - 1;
            Level data = levelData.Levels[index];
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CLevel level = Object.Instantiate(prefab).GetComponent<CLevel>();
            level.SetTime(data.Time);
            level.SetLoot(data.Loot);
            _levelModel.SetLevel(level);
            return level;
        }

        async UniTask<CCharacter> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
        {
            CharacterData data = _staticDataService.CharacterData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CCharacter character = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CCharacter>();
            character.Health.SetBaseHealth(data.Health);
            character.CharacterController.SetBaseSpeed(data.Speed);
            _levelModel.SetCharacter(character);
            return character;
        }
        
        async UniTask<CUnit> IGameFactory.CreateUnit(Vector3 position, Transform parent)
        {
            UnitData data = _staticDataService.UnitData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.Prefabreference);
            CUnit unit = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CUnit>();
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
    }
}