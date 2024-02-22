using CodeBase.Game.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(UpgradeButtonData), menuName = "Data/" + nameof(UpgradeButtonData))]
    public sealed class UpgradeButtonData : ScriptableObject
    {
        public UpgradeButtonType UpgradeButtonType;
        [Range(1, 2500)]public int BaseCost;
        public AssetReference PrefabReference;
    }
}