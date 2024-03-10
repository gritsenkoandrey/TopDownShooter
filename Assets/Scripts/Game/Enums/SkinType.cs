using System;

namespace CodeBase.Game.Enums
{
    [Serializable]
    public enum SkinType : byte
    {
        BasicMale   = 0,
        BasicFemale = 1,
        Alien       = 2,
        Bear        = 3,
        Chemical    = 4,
        Chicken     = 5,
        Cowboy      = 6,
        Hero        = 7,
        Hoodie      = 8,
        Jester      = 9,
        Knight      = 10,
        Ninja       = 11,
        Robot       = 12,
        Salary      = 13,
        Samurai     = 14,
        Sniper      = 15,
        Solder      = 16,
        Spaceman    = 17,
        Terrorist   = 18,
        Veteran     = 19,
        
        None        = byte.MaxValue
    }
}