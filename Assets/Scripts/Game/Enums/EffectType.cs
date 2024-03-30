﻿namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum EffectType : byte
    {
        Hit   = 0,
        Death = 1,
        Blood = 2,
        
        None = byte.MaxValue
    }
}