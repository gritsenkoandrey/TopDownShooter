using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [System.Serializable]
    public struct WeaponCharacteristic
    {
        [Range(1,100)] public int Damage;
        [Range(1,100)] public int ClipCount;
        [Range(0f,10f)] public float ForceBullet;
        [Range(0f,10f)] public float RechargeTime;
        [Range(0f,10f)] public float FireInterval;
        [Range(0f,100f)] public int CriticalChance;
        [Range(0f,100f)] public float CriticalMultiplier;
        [Range(0f,20f)] public float DetectionDistance;
        [Range(0f,20f)] public float AttackDistance;
        public bool IsDetectThroughObstacle;
    }
}