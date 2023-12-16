using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(WeaponCharacteristicData), menuName = "Data/" + nameof(WeaponCharacteristicData))]
    public sealed class WeaponCharacteristicData : ScriptableObject
    {
        public WeaponType WeaponType;
        public WeaponCharacteristic WeaponCharacteristic;
    }
}