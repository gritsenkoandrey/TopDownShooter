using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Data/ZombieData")]
    public sealed class ZombieData : ScriptableObject
    {
        public ZombieType ZombieType;
        [Range(1, 20)] public int Health;
        [Range(1, 5)] public int Damage;
        [Range(1f, 5f)] public float WalkSpeed;
        [Range(1, 10f)] public float RunSpeed;
        [Range(0.1f, 2f)] public float AttackDelay;
        public CEnemy Prefab;
    }
}