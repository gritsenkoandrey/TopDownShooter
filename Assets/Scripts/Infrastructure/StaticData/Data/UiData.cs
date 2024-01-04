using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(UiData), menuName = "Data/" + nameof(UiData))]
    public sealed class UiData : ScriptableObject
    {
        public AssetReference EnemyHealthPrefabReference;
        public AssetReference PointerArrowPrefabReference;
    }
}