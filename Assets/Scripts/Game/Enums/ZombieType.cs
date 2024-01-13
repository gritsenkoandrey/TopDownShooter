namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum ZombieType : byte
    {
        Easy   = 0,
        Normal = 1,
        Hard   = 2,
        Boss   = 3,
        
        None   = byte.MaxValue
    }
}