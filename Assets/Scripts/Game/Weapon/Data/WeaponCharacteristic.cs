namespace CodeBase.Game.Weapon.Data
{
    [System.Serializable]
    public struct WeaponCharacteristic
    {
        public int Damage;
        public int ClipCount;
        public float ForceBullet;
        public float RechargeTime;
        public float SpeedAttack;
        public float DetectionDistance;
        public float AttackDistance;
        public bool IsDetectThroughObstacle;
    }
}