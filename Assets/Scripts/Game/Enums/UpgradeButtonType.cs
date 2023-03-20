namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum UpgradeButtonType : byte
    {
        None   = byte.MaxValue,
        Damage = 0,
        Health = 1,
        Speed  = 2,
    }
}