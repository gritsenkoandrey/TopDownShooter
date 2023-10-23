using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.AssetData
{
    public sealed class AssetService : IAssetService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cashHandles = new ();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new ();

        async UniTask IAssetService.Init() => await Addressables.InitializeAsync();

        T IAssetService.LoadFromResources<T>(string path) => Resources.Load<T>(path);
        
        T[] IAssetService.LoadAllFromResources<T>(string path) => Resources.LoadAll<T>(path);
        
        async UniTaskVoid IAssetService.Unload()
        {
            foreach (List<AsyncOperationHandle> handles in _handles.Values)
            foreach (AsyncOperationHandle handle in handles)
            {
                Addressables.Release(handle);
            }
            
            _cashHandles.Clear();
            _handles.Clear();

            await Resources.UnloadUnusedAssets();
        }

        async UniTask<T> IAssetService.LoadFromAddressable<T>(AssetReference assetReference) where T : class
        {
            if (_cashHandles.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle cashedHandle))
            {
                return cashedHandle.Result as T;
            }
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

            handle.Completed += OnHandleCompleted<T>(assetReference.AssetGUID);

            AddHandle(handle, assetReference.AssetGUID);

            return await handle.ToUniTask();
        }
        
        async UniTask<T> IAssetService.LoadFromAddressable<T>(string address) where T : class
        {
            if (_cashHandles.TryGetValue(address, out AsyncOperationHandle cashedHandle))
            {
                return cashedHandle.Result as T;
            }
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            handle.Completed += OnHandleCompleted<T>(address);

            AddHandle(handle, address);

            return await handle.ToUniTask();
        }

        private Action<AsyncOperationHandle<T>> OnHandleCompleted<T>(string address) where T : class
        {
            return handle => _cashHandles[address] = handle;
        }

        private void AddHandle<T>(AsyncOperationHandle<T> handle, string key) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> handles))
            {
                handles = new List<AsyncOperationHandle>();

                _handles[key] = handles;
            }

            handles.Add(handle);
        }
    }
}