namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum UnitType : byte
    {
        None      = byte.MaxValue,
        Character = 0,
        Zombie    = 1,
    }
}