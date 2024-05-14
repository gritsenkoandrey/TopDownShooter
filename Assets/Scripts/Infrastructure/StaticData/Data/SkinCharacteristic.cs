using System;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [Serializable]
    public struct SkinCharacteristic
    {
        public int BaseHealth;
        public float BaseSpeed;
        public int RegenerationHealth;
        public float RegenearationInterval;
    }
}