using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [System.Serializable]
    public struct UnitStats
    {
        [Range(1, 500)] public int Money;
        [Range(1, 500)] public int Health;
        [Range(1f, 5f)] public float WalkSpeed;
        [Range(1f, 10f)] public float PursuitSpeed;
        [Range(1f, 10f)] public float StayDelay;
        [Range(2f, 20f)] public float PursuitRadius;
        [Range(2f, 20f)] public float PatrolRadius;
        [Range(1f, 10f)] public float Height;
        [Range(1f, 10f)] public float Scale;
    }
}