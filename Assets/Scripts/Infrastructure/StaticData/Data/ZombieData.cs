using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ZombieData), menuName = "Data/" + nameof(ZombieData))]
    public sealed class ZombieData : ScriptableObject
    {
        [Space] public ZombieType ZombieType;
        [Range(1, 250)] public int Health;
        [Range(1, 25)] public int Damage;
        public EnemyStats Stats;
        [Space] public CZombie Prefab;
    }
}