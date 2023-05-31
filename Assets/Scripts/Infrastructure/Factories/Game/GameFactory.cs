using System.Collections.Generic;
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

        public GameFactory(IStaticDataService staticDataService, IProgressService progressService, IObjectPoolService objectPoolService, ICameraService cameraService)
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

            _character = Object.Instantiate(characterData.Prefab, _staticDataService.LevelData(levelType).Prefab.CharacterSpawnPosition, Quaternion.identity);

            _character.Health.BaseHealth = characterData.Health;
            _character.Weapon.BaseDamage = characterData.Damage;
            _character.Weapon.AttackDistance = characterData.AttackDistance;
            _character.Weapon.AttackRecharge = characterData.AttackRecharge;
            _character.Move.BaseSpeed = characterData.Speed;
                
            Registered(_character.Health);
            Registered(_character.Weapon);
            Registered(_character.Move);
            
            _cameraService.SetTarget(_character.transform);

            return _character;
        }

        CZombie IGameFactory.CreateZombie(ZombieType zombieType, Vector3 position, Transform parent)
        {
            ZombieData data = _staticDataService.ZombieData(zombieType);
            
            CZombie zombie = Object.Instantiate(data.Prefab, position, Quaternion.identity, parent);

            zombie.Health.MaxHealth = data.Health;
            zombie.Health.Health.Value = data.Health;
            zombie.Melee.Damage = data.Damage;
            zombie.Stats = data.Stats;
            zombie.Radar.Radius = data.Stats.AggroRadius;
            
            _character.Enemies.Add(zombie);
            
            zombie.Target.SetValueAndForceNotify(_character);

            return zombie;
        }

        CBullet IGameFactory.CreateBullet(Vector3 position)
        {
            return _objectPoolService.SpawnObject(_staticDataService.BulletData().gameObject, position, Quaternion.identity).GetComponent<CBullet>();
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
            }
            
            if (_level != null)
            {
                Object.Destroy(_level.gameObject);
            }

            _level = null;
            _character = null;
            
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