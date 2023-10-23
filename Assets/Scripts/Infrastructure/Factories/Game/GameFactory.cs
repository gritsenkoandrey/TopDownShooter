using System.Collections.Generic;
using CodeBase.Game.Behaviours;
using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
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
        private readonly IAssetService _assetService;

        private CLevel _level;
        private CCharacter _character;
        private readonly IReactiveCollection<IEnemy> _enemies = new ReactiveCollection<IEnemy>();

        public GameFactory(IStaticDataService staticDataService, IProgressService progressService, 
            IObjectPoolService objectPoolService, ICameraService cameraService, IAssetService assetService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _objectPoolService = objectPoolService;
            _cameraService = cameraService;
            _assetService = assetService;
        }

        public IList<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public IList<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();

        ILevel IGameFactory.Level => _level;
        ICharacter IGameFactory.Character => _character;
        IReactiveCollection<IEnemy> IGameFactory.Enemies => _enemies;

        async UniTask<ILevel> IGameFactory.CreateLevel()
        {
            LevelData data = _staticDataService.LevelData(GetLevelType());

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            _level = new LevelBuilder()
                .Reset()
                .SetPrefab(prefab.GetComponent<CLevel>())
                .SetLevelType(data.LevelType)
                .SetLevelTime(data.LevelTime)
                .Build();

            foreach (SpawnPoint spawnPoint in _level.SpawnPoints)
            {
                switch (spawnPoint.UnitType)
                {
                    case UnitType.None:
                        break;
                    case UnitType.Character:
                        await CreateCharacter(spawnPoint.Position, _level.transform);
                        break;
                    case UnitType.Zombie:
                        await CreateZombie(spawnPoint.ZombieType, spawnPoint.Position, _level.transform);
                        break;
                }
            }
            
            SubscribeOnCreateEnemies();
            
            return _level;
        }

        private async UniTask<ICharacter> CreateCharacter(Vector3 position, Transform parent)
        {
            CharacterData data = _staticDataService.CharacterData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            _character = new CharacterBuilder()
                .Reset()
                .SetPrefab(prefab.GetComponent<CCharacter>())
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

        private async UniTask<CZombie> CreateZombie(ZombieType zombieType, Vector3 position, Transform parent)
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
            
            _enemies.Add(zombie);

            return zombie;
        }

        async UniTask<IBullet> IGameFactory.CreateBullet(int damage, Vector3 position, Vector3 direction)
        {
            BulletData data = _staticDataService.BulletData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.Bullet);

            return new BulletBuilder(_objectPoolService)
                .SetPrefab(prefab)
                .SetDamage(damage)
                .SetPosition(position)
                .SetDirection(direction)
                .SetCollisionDistance(data.CollisionRadius)
                .Build();
        }

        async UniTask<GameObject> IGameFactory.CreateHitFx(Vector3 position)
        {
            FxData data = _staticDataService.FxData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.HitFx);

            GameObject hitFx = _objectPoolService.SpawnObject(prefab, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(hitFx, data.HitFxReleaseTime);
            
            return hitFx;
        }

        async UniTask<GameObject> IGameFactory.CreateDeathFx(Vector3 position)
        {
            FxData data = _staticDataService.FxData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.DeathFx);

            GameObject deathFx = _objectPoolService.SpawnObject(prefab, position, Quaternion.identity);
            
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

        private void SubscribeOnCreateEnemies()
        {
            foreach (IEnemy enemy in _enemies)
            {
                enemy.Target.SetValueAndForceNotify(_character);
            }

            _enemies
                .ObserveAdd()
                .Subscribe(enemy => enemy.Value.Target.SetValueAndForceNotify(_character))
                .AddTo(_level.LifetimeDisposable);
        }
    }
}