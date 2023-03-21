namespace CodeBase.UI.Screens
{
    [System.Serializable]
    public enum ScreenType : byte
    {
        None = byte.MaxValue,
        
        Lobby  = 0,
        Game   = 1,
        Win    = 2,
        Lose   = 4,
    }
}