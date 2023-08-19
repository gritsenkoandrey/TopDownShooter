using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public CLevel Level { get; }
        public CCharacter Character { get; }
        public IList<IProgressReader> ProgressReaders { get; }
        public IList<IProgressWriter> ProgressWriters { get; }
        public CLevel CreateLevel();
        public CCharacter CreateCharacter();
        public CZombie CreateZombie(ZombieType zombieType, Vector3 position, Transform parent);
        public CBullet CreateBullet(Vector3 position);
        public GameObject CreateHitFx(Vector3 position);
        public GameObject CreateDeathFx(Vector3 position);
        public void CleanUp();
    }
}