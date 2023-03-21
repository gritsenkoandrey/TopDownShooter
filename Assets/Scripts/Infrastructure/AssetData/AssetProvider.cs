using UnityEngine;

namespace CodeBase.Infrastructure.AssetData
{
    public sealed class AssetProvider : IAsset
    {
        public T Load<T>(string path) where T : class
        {
            return Resources.Load(path, typeof(T)) as T;
        }
    }
}