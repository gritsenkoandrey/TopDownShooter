using CodeBase.Game.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(WeaponCharacteristicData), menuName = "Data/" + nameof(WeaponCharacteristicData))]
    public sealed class WeaponCharacteristicData : ScriptableObject
    {
        [Space] public WeaponType WeaponType;
        public WeaponCharacteristic WeaponCharacteristic;
        [Space] public AssetReference PrefabReference;
    }
}