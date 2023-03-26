namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum LevelType : byte
    {
        None   = byte.MaxValue,
        Normal = 0,
        Boss   = 1,
    }
}