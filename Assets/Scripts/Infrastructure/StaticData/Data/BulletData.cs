using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(BulletData), menuName = "Data/" + nameof(BulletData))]
    public sealed class BulletData : ScriptableObject
    {
        public float CollisionRadius;
    }
}