namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum UpgradeButtonType : byte
    {
        Damage = 0,
        Health = 1,
        Speed  = 2,
        
        None   = byte.MaxValue
    }
}