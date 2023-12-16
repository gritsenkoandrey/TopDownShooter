using System.Collections.Generic;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
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
        public UniTask<ILevel> CreateLevel();
        public UniTask<GameObject> CreateHitFx(Vector3 position);
        public UniTask<GameObject> CreateDeathFx(Vector3 position);
        public void CleanUp();
    }
}