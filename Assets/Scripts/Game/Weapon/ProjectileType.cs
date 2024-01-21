namespace CodeBase.Game.Weapon
{
    [System.Serializable]
    public enum ProjectileType : byte
    {
        Bullet     = 0,
        Rocket     = 1,
        LaserFire  = 2,
        LaserBlue  = 3,
        Frost      = 4,
        Lighting   = 5,
        Fireball   = 6,
        Chemical   = 7,
         
        None = byte.MaxValue
    }
}