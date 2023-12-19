namespace CodeBase.Game.Weapon
{
    [System.Serializable]
    public enum WeaponType : byte
    {
        Melee = 0,
        Rifle = 1,
        SniperRifle = 2,
        Pistol = 3,
        HuntingRifle = 4,
        
        None = byte.MaxValue
    }
}