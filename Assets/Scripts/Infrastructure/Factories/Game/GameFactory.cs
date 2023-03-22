using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public sealed class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();
        public CLevel CurrentLevel { get; private set; }
        public CCharacter CurrentCharacter { get; private set; }

        public GameFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public CLevel CreateLevel()
        {
            return CurrentLevel = Object.Instantiate(_staticDataService.LevelData());
        }

        public CCharacter CreateCharacter()
        {
            CharacterData characterData = _staticDataService.CharacterData();
            
            CurrentCharacter = Object.Instantiate(characterData.Prefab, _staticDataService.LevelData().CharacterSpawnPosition, Quaternion.identity);

            CurrentCharacter.Health.BaseHealth = characterData.Health;
            CurrentCharacter.Weapon.BaseDamage = characterData.Damage;
            CurrentCharacter.Weapon.AttackDistance = characterData.AttackDistance;
            CurrentCharacter.Weapon.AttackRecharge = characterData.AttackRecharge;
            CurrentCharacter.Move.BaseSpeed = characterData.Speed;
                
            Registered(CurrentCharacter.Health);
            Registered(CurrentCharacter.Weapon);
            Registered(CurrentCharacter.Move);

            return CurrentCharacter;
        }

        public CEnemy CreateZombie(ZombieType zombieType, Vector3 position, Transform parent)
        {
            ZombieData data = _staticDataService.ZombieData(zombieType);
            
            CEnemy zombie = Object.Instantiate(data.Prefab, position, Quaternion.identity, parent);
            
            zombie.Construct(CurrentCharacter);

            zombie.Health.MaxHealth = data.Health;
            zombie.Health.Health.Value = data.Health;
            zombie.Melee.Damage = data.Damage;
            zombie.Stats = data.Stats;
            
            CurrentCharacter.Enemies.Add(zombie);

            return zombie;
        }

        public CBullet CreateBullet(Vector3 position)
        {
            return Object.Instantiate(_staticDataService.BulletData(), position, Quaternion.identity);
        }

        public Transform CreateHitFx(Vector3 position)
        {
            return Object.Instantiate(_staticDataService.FxData().HitFx, position, Quaternion.identity);
        }

        public Transform CreateDeathFx(Vector3 position)
        {
            return Object.Instantiate(_staticDataService.FxData().DeatFx, position, Quaternion.identity);
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

        public void CleanUp()
        {
            if (CurrentCharacter != null)
            {
                Object.Destroy(CurrentCharacter.gameObject);
            }
            
            if (CurrentLevel != null)
            {
                Object.Destroy(CurrentLevel.gameObject);
            }

            CurrentLevel = null;
            CurrentCharacter = null;
            
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}