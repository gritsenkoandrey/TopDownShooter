using CodeBase.Game.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(LevelData), menuName = "Data/" + nameof(LevelData))]
    public sealed class LevelData : ScriptableObject
    {
        public LevelType LevelType;
        public int LevelTime;
        public AssetReference PrefabReference;
    }
}