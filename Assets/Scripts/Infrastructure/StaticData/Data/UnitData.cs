using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(UnitData), menuName = "Data/" + nameof(UnitData))]
    public sealed class UnitData : ScriptableObject
    {
        public AssetReference Prefabreference;
    }
}