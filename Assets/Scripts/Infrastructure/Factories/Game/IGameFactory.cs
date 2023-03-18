using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.LevelData;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public Level CurrentLevel { get; }
        public ICharacter CurrentCharacter { get; }
        public Level CreateLevel();
        public ICharacter CreateCharacter();
        public CEnemy CreateZombie(ZombieType zombieType, Vector3 position, Transform parent);
        public IBullet CreateBullet(Vector3 position);
        public void CleanUp();
    }
}