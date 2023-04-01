using System;

namespace CodeBase.Infrastructure.Loader
{
    public interface ISceneLoader
    {
        public void Load(string name, Action onLoaded = null);
    }
}