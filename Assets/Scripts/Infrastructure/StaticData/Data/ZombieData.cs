using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Data/ZombieData")]
    public sealed class ZombieData : ScriptableObject
    {
        [Space] public ZombieType ZombieType;
        [Range(1, 100)] public int Health;
        [Range(1, 10)] public int Damage;
        public ZombieStats Stats;
        [Space] public CEnemy Prefab;
    }
}