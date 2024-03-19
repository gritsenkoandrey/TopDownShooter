using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetData
{
    public interface IAssetService
    {
        UniTask Init();
        T LoadFromResources<T>(string path) where T : Object;
        T[] LoadAllFromResources<T>(string path) where T : Object;
        UniTask<T> LoadFromAddressable<T>(AssetReference assetReference) where T : class;
        UniTask<T> LoadFromAddressable<T>(string address) where T : class;
        UniTaskVoid CleanUp();
    }
}