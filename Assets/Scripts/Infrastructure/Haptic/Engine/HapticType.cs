namespace CodeBase.Infrastructure.Haptic.Engine
{
    [System.Serializable]
    public enum HapticType : byte
    {
        Success   = 0,
        Warning   = 1,
        Failure   = 2,
        Light     = 3,
        Medium    = 4,
        Heavy     = 5,
        Vibrate   = 6,
        Selection = 7,
        
        None = byte.MaxValue
    }
}