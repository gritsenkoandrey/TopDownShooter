using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [System.Serializable]
    public struct ZombieStats
    {
        [Range(1, 500)] public int Money;
        [Range(1f, 5f)] public float WalkSpeed;
        [Range(1, 10f)] public float RunSpeed;
        [Range(0.1f, 2f)] public float AttackDelay;
        [Range(1f, 5f)] public float StayDelay;
        [Range(1f, 10f)] public float AggroRadius;
        [Range(2.5f, 20f)] public float PursuitRadius;
        [Range(2.5f, 20f)] public float PatrolRadius;
        [Range(1f, 3f)] public float MinDistanceToTarget;
        [Range(1f, 10f)] public float Height;
    }
}