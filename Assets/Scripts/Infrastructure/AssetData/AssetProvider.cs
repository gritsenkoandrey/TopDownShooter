using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetData
{
    public sealed class AssetProvider : IAsset
    {
        public T Load<T>(string path) where T : Object => Resources.Load<T>(path);
        public T[] LoadAll<T>(string path) where T : Object => Resources.LoadAll<T>(path);
        public async void Unload() => await Resources.UnloadUnusedAssets();
    }
}