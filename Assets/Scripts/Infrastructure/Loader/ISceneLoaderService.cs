using System;

namespace CodeBase.Infrastructure.Loader
{
    public interface ISceneLoaderService
    {
        public void Load(string name, Action onLoaded);
    }
}