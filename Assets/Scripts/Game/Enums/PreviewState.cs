using System;

namespace CodeBase.Game.Enums
{
    [Serializable]
    public enum PreviewState : byte
    {
        Start       = 0,
        BuyWeapon   = 1,
        BuySkin     = 2,
        BuyUpgrades = 3,
        DailyTask   = 4,
    }
}