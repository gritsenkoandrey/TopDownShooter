using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(SkinCharacteristicData), menuName = "Data/" + nameof(SkinCharacteristicData))]
    public sealed class SkinCharacteristicData : ScriptableObject
    {
        public SkinType SkinType;
        public SkinCharacteristic SkinCharacteristic;
    }
}