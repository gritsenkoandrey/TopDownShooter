namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum EffectType : byte
    {
        Hit   = 0,
        Death = 1,
        
        None = byte.MaxValue
    }
}