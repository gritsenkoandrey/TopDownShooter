using System.Collections.Generic;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Factories.Game
{
    public interface IGameFactory : IService
    {
        public IList<IProgressReader> ProgressReaders { get; }
        public IList<IProgressWriter> ProgressWriters { get; }
        public UniTask<ILevel> CreateLevel();
        public void CleanUp();
    }
}