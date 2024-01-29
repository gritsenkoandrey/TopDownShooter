﻿using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
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
            Level data = GetLevel(_staticDataService.LevelData());

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CLevel level = new LevelBuilder()
                .SetPrefab(prefab.GetComponent<CLevel>())
                .SetLevelTime(data.LevelTime)
                .Build();
            
            _levelModel.SetLevel(level);
            
            return level;
        }

        async UniTask<CCharacter> IGameFactory.CreateCharacter(Vector3 position, Transform parent)
        {
            CharacterData data = _staticDataService.CharacterData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CCharacter character = new CharacterBuilder()
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
        
        async UniTask<CUnit> IGameFactory.CreateUnit(Vector3 position, Transform parent)
        {
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.Unit);

            CUnit unit = Object.Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<CUnit>();
            
            _levelModel.AddEnemy(unit);

            return unit;
        }

        void IGameFactory.CleanUp() { }

        private Level GetLevel(LevelData data)
        {
            int index;

            if (_progressService.LevelData.Data.Value >= data.Levels.Length)
            {
                index = _progressService.LevelData.Data.Value % data.Levels.Length;
            }
            else
            {
                index = _progressService.LevelData.Data.Value;
            }
            
            return data.Levels[index];
        }
    }
}