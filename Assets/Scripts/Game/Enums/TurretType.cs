namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum TurretType : byte
    {
        Canon   = 0,
        Gatling = 1,
        Mortal  = 2,
        
        None    = byte.MaxValue
    }
}