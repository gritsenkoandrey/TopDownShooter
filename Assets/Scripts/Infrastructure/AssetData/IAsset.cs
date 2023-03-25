using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Infrastructure.AssetData
{
    public interface IAsset : IService
    {
        public T Load<T>(string path) where T : Object;
        public T[] LoadAll<T>(string path) where T : Object;
        public void Unload();
    }
}