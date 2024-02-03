namespace CodeBase.UI.Screens
{
    [System.Serializable]
    public enum ScreenType : byte
    {
        Lobby   = 0,
        Game    = 1,
        Win     = 2,
        Lose    = 3,
        Preview = 4,
        
        None = byte.MaxValue
    }
}