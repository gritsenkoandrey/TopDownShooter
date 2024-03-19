using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    public interface IObjectPoolService
    {
        UniTask Init();
        void Log();
        GameObject SpawnObject(GameObject prefab);
        GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
        void ReleaseObject(GameObject clone);
        UniTaskVoid ReleaseObjectAfterTime(GameObject clone, float time);
    }
}