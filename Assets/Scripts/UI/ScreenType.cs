namespace CodeBase.UI
{
    [System.Serializable]
    public enum ScreenType : byte
    {
        None = byte.MaxValue,
        
        Lobby  = 0,
        Game   = 1,
        Result = 2,
    }
}