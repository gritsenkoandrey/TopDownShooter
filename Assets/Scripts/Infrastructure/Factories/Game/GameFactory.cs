using System.Collections.Generic;
using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using UniRx;
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
        private readonly IReactiveCollection<IEnemy> _enemies = new ReactiveCollection<IEnemy>();

        public GameFactory(IStaticDataService staticDataService, IProgressService progressService, 
            IObjectPoolService objectPoolService, ICameraService cameraService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _objectPoolService = objectPoolService;
            _cameraService = cameraService;
        }

        public IList<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public IList<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();

        ILevel IGameFactory.Level => _level;
        ICharacter IGameFactory.Character => _character;
        IReactiveCollection<IEnemy> IGameFactory.Enemies => _enemies;

        ILevel IGameFactory.CreateLevel()
        {
            LevelData data = _staticDataService.LevelData(GetLevelType());

            _level = new LevelBuilder()
                .Reset()
                .SetPrefab(data.Prefab)
                .SetLevelType(data.LevelType)
                .SetLevelTime(data.LevelTime)
                .Build();
            
            return _level;
        }

        ICharacter IGameFactory.CreateCharacter(Vector3 position, Transform parent)
        {
            CharacterData data = _staticDataService.CharacterData();

            _character = new CharacterBuilder()
                .Reset()
                .SetPrefab(data.Prefab)
                .SetParent(parent)
                .SetCamera(_cameraService)
                .SetPosition(position)
                .SetHealth(data.Health)
                .SetDamage(data.Damage)
                .SetAttackDistance(data.AttackDistance)
                .SetAttackRecharge(data.AttackRecharge)
                .SetSpeed(data.Speed)
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
                .Build();
            
            _enemies.Add(zombie);

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
            FxData data = _staticDataService.FxData();

            GameObject hitFx = _objectPoolService.SpawnObject(data.HitFx, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(hitFx, data.HitFxReleaseTime);
            
            return hitFx;
        }

        GameObject IGameFactory.CreateDeathFx(Vector3 position)
        {
            FxData data = _staticDataService.FxData();
            
            GameObject deathFx = _objectPoolService.SpawnObject(data.DeatFx, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(deathFx, data.DeathFxReleaseTime);

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
            
            _enemies.Clear();
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

        private LevelType GetLevelType()
        {
            return _progressService.PlayerProgress.Level % 5 == 0 ? LevelType.Boss : LevelType.Normal;
        }
    }
}