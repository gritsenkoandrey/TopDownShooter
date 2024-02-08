namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum WeaponType : byte
    {
        Knife        = 0,
        Rifle        = 1,
        SniperRifle  = 2,
        Pistol       = 3,
        HuntingRifle = 4,
        Bazooka      = 5,
        ElectricGun  = 6,
        Shotgun      = 7,
        SMG          = 8,
        RailGun      = 9,
        MiniGun      = 10,
        IceGun       = 11,
        HMG          = 12,
        GravityGun   = 13,
        FlameThrower = 14,
        ChemicalGun  = 15,
        
        None         = byte.MaxValue
    }
}