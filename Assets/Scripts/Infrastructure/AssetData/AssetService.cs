using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetData
{
    public sealed class AssetService : IAssetService
    {
        T IAssetService.Load<T>(string path) => Resources.Load<T>(path);
        T[] IAssetService.LoadAll<T>(string path) => Resources.LoadAll<T>(path);
        async void IAssetService.Unload() => await Resources.UnloadUnusedAssets();
    }
}