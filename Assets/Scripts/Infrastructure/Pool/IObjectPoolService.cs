using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    public interface IObjectPoolService
    {
        public UniTask Init();
        public void Log();
        public GameObject SpawnObject(GameObject prefab);
        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
        public void ReleaseObject(GameObject clone);
        public UniTaskVoid ReleaseObjectAfterTime(GameObject clone, float time);
    }
}