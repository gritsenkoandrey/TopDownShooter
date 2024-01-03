using System.Collections.Generic;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public IList<IProgressReader> ProgressReaders { get; }
        public IList<IProgressWriter> ProgressWriters { get; }
        public UniTask<ILevel> CreateLevel();
        public UniTask<ICharacter> CreateCharacter(Vector3 position, Transform parent);
        public UniTask<CZombie> CreateZombie(ZombieType zombieType, Vector3 position, Transform parent);
        public void CleanUp();
    }
}