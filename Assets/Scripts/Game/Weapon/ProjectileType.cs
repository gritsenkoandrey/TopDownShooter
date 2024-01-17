namespace CodeBase.Game.Weapon
{
    [System.Serializable]
    public enum ProjectileType : byte
    {
        Bullet = 0,
        Rocket = 1,
        
        None = byte.MaxValue
    }
}