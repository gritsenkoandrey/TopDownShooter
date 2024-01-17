using UnityEngine;

namespace CodeBase.Game.Weapon.Data
{
    [System.Serializable]
    public struct WeaponCharacteristic
    {
        public int Damage;
        public int ClipCount;
        public float ForceBullet;
        public float RechargeTime;
        public float FireInterval;
        [Range(0f,100f)] public int CriticalChance;
        public float CriticalMultiplier;
        public float DetectionDistance;
        public float AttackDistance;
        public bool IsDetectThroughObstacle;
    }
}