using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Pool
{
    [System.Serializable]
    public struct ObjectPoolData
    {
        public AssetReference PrefabReference;
        public int Count;
    }
}