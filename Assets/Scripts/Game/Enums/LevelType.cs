namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum LevelType : byte
    {
        Normal = 0,
        Boss   = 1,
        
        None   = byte.MaxValue
    }
}