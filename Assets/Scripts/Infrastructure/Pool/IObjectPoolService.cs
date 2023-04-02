using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    public interface IObjectPoolService : IService
    {
        public GameObject SpawnObject(GameObject prefab);
        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
        public void ReleaseObject(GameObject clone);
    }
}