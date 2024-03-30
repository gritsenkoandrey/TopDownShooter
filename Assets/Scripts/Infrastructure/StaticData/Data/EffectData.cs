using CodeBase.Game.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(EffectData), menuName = "Data/" + nameof(EffectData))]
    public sealed class EffectData : ScriptableObject
    {
        public EffectType EffectType;
        [Range(0.1f, 10f)]public float LifeTime;
        public AssetReference PrefabReference;
    }
}