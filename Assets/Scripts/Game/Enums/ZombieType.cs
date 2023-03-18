namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum ZombieType : byte
    {
        None   = byte.MaxValue,
        Easy   = 0,
        Normal = 1,
        Hard   = 2,
    }
}