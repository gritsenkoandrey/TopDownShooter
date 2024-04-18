using CodeBase.Game.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(TurretData), menuName = "Data/" + nameof(TurretData))]
    public sealed class TurretData : ScriptableObject
    {
        public TurretType Type;
        public AssetReference PrefabReference;
    }
}