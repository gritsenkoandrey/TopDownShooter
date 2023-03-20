using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.LevelData;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public Level CurrentLevel { get; }
        public CCharacter CurrentCharacter { get; }
        public List<IProgressReader> ProgressReaders { get; }
        public List<IProgressWriter> ProgressWriters { get; }
        public Level CreateLevel();
        public CCharacter CreateCharacter();
        public CEnemy CreateZombie(ZombieType zombieType, Vector3 position, Transform parent);
        public IBullet CreateBullet(Vector3 position);
        public void CleanUp();
    }
}