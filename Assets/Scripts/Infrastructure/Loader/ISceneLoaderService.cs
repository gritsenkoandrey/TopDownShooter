using System;

namespace CodeBase.Infrastructure.Loader
{
    public interface ISceneLoaderService
    {
        void Load(string name, Action onLoaded);
    }
}