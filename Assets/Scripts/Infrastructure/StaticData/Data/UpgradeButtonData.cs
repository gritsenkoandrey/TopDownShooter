using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(UpgradeButtonData), menuName = "Data/" + nameof(UpgradeButtonData))]
    public sealed class UpgradeButtonData : ScriptableObject
    {
        public UpgradeButtonType UpgradeButtonType;
        [Range(1, 500)]public int BaseCost;
        public CUpgradeButton Prefab;
    }
}