using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Loader
{
    public interface ISceneLoaderService : IService
    {
        public void Load(string name, Action onLoaded);
    }
}