using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
    public interface IObjectPoolService
    {
        UniTask Init();
        void Execute();
        GameObject SpawnObject(GameObject prefab);
        GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
        void ReleaseObject(GameObject clone);
        void ReleaseObjectAfterTime(GameObject clone, float time);
        void ReleaseAll();
    }
}