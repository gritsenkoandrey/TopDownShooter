using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    [System.Serializable]
    public struct ObjectPoolItem
    {
        public GameObject Prefab;
        public int Count;
    }
}