using CodeBase.Game.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ProjectileData), menuName = "Data/" + nameof(ProjectileData))]
    public sealed class ProjectileData : ScriptableObject
    {
        public ProjectileType ProjectileType;
        public float CollisionRadius;
        public float LifeTime;
        public AssetReference PrefabReference;
    }
}