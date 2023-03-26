using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 0)]
    public sealed class LevelData : ScriptableObject
    {
        public LevelType LevelType;
        public CLevel Prefab;
    }
}