using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(PreviewData), menuName = "Data/" + nameof(PreviewData))]
    public sealed class PreviewData : ScriptableObject
    {
        public AssetReference PrefabReference;
    }
}