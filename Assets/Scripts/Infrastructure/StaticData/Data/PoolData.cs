using CodeBase.Infrastructure.Pool;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(PoolData), menuName = "Data/" + nameof(PoolData))]
    public sealed class PoolData : ScriptableObject
    {
        public bool LogStatus;
        
        public ObjectPoolData[] PoolItems;
    }
}