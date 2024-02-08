using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetData
{
    public interface IAssetService
    {
        public UniTask Init();
        public T LoadFromResources<T>(string path) where T : Object;
        public T[] LoadAllFromResources<T>(string path) where T : Object;
        UniTask<T> LoadFromAddressable<T>(AssetReference assetReference) where T : class;
        UniTask<T> LoadFromAddressable<T>(string address) where T : class;
        public UniTaskVoid CleanUp();
    }
}