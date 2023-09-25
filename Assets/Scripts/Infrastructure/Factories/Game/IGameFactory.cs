using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public ILevel Level { get; }
        public ICharacter Character { get; }
        public IReactiveCollection<IEnemy> Enemies { get; }
        public IList<IProgressReader> ProgressReaders { get; }
        public IList<IProgressWriter> ProgressWriters { get; }
        public ILevel CreateLevel();
        public ICharacter CreateCharacter(Vector3 position, Transform parent);
        public CZombie CreateZombie(ZombieType zombieType, Vector3 position, Transform parent);
        public CBullet CreateBullet(Vector3 position);
        public GameObject CreateHitFx(Vector3 position);
        public GameObject CreateDeathFx(Vector3 position);
        public void CleanUp();
    }
}