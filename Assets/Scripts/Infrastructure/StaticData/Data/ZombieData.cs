using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Data/ZombieData")]
    public sealed class ZombieData : ScriptableObject
    {
        [Space] public ZombieType ZombieType;
        [Range(1, 250)] public int Health;
        [Range(1, 25)] public int Damage;
        public ZombieStats Stats;
        [Space] public CZombie Prefab;
    }
}