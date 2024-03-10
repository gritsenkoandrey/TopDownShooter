using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(LevelData), menuName = "Data/" + nameof(LevelData))]
    public sealed class LevelData : ScriptableObject
    {
        public Level[] Levels;
    }

    [System.Serializable]
    public struct Level
    {
        public int Time;
        public int Loot;
        public AssetReference PrefabReference;
    }
}