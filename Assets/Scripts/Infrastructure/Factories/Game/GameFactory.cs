using System.Collections.Generic;
using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressService _progressService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly ICameraService _cameraService;

        private CLevel _level;
        private CCharacter _character;

        public GameFactory(IStaticDataService staticDataService, IProgressService progressService, 
            IObjectPoolService objectPoolService, ICameraService cameraService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _objectPoolService = objectPoolService;
            _cameraService = cameraService;
        }

        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();

        CLevel IGameFactory.Level => _level;
        CCharacter IGameFactory.Character => _character;

        CLevel IGameFactory.CreateLevel()
        {
            LevelType levelType = _progressService.PlayerProgress.Level % 5 == 0 ? LevelType.Boss : LevelType.Normal;
            
            return _level = Object.Instantiate(_staticDataService.LevelData(levelType).Prefab);
        }

        CCharacter IGameFactory.CreateCharacter()
        {
            CharacterData characterData = _staticDataService.CharacterData();
            
            LevelType levelType = _progressService.PlayerProgress.Level % 5 == 0 ? LevelType.Boss : LevelType.Normal;

            _character = new CharacterBuilder()
                .Reset()
                .SetPrefab(characterData.Prefab)
                .SetCamera(_cameraService)
                .SetPosition(_staticDataService.LevelData(levelType).Prefab.CharacterSpawnPosition)
                .SetHealth(characterData.Health)
                .SetDamage(characterData.Damage)
                .SetAttackDistance(characterData.AttackDistance)
                .SetAttackRecharge(characterData.AttackRecharge)
                .SetSpeed(characterData.Speed)
                .Build();

            Registered(_character.Health);
            Registered(_character.Weapon);
            Registered(_character.Move);

            return _character;
        }

        CZombie IGameFactory.CreateZombie(ZombieType zombieType, Vector3 position, Transform parent)
        {
            ZombieData data = _staticDataService.ZombieData(zombieType);

            CZombie zombie = new ZombieBuilder()
                .Reset()
                .SetPrefab(data.Prefab)
                .SetPosition(position)
                .SetParent(parent)
                .SetHealth(data.Health)
                .SetDamage(data.Damage)
                .SetStats(data.Stats)
                .SetTarget(_character)
                .Build();
            
            _character.Enemies.Add(zombie);

            return zombie;
        }

        CBullet IGameFactory.CreateBullet(Vector3 position)
        {
            return _objectPoolService
                .SpawnObject(_staticDataService.BulletData().gameObject, position, Quaternion.identity)
                .GetComponent<CBullet>();
        }

        GameObject IGameFactory.CreateHitFx(Vector3 position)
        {
            GameObject hitFx = _objectPoolService.SpawnObject(_staticDataService.FxData().HitFx, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(hitFx, 1f);
            
            return hitFx;
        }

        GameObject IGameFactory.CreateDeathFx(Vector3 position)
        {
            GameObject deathFx = _objectPoolService.SpawnObject(_staticDataService.FxData().DeatFx, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(deathFx, 2f);

            return deathFx;
        }

        void IGameFactory.CleanUp()
        {
            if (_character != null)
            {
                Object.Destroy(_character.gameObject);

                _character = null;
            }
            
            if (_level != null)
            {
                Object.Destroy(_level.gameObject);

                _level = null;
            }
            
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Registered(IProgress progress)
        {
            if (progress is IProgressWriter writer)
            {
                ProgressWriters.Add(writer);
            }

            if (progress is IProgressReader reader)
            {
                ProgressReaders.Add(reader);
            }
        }
    }
}