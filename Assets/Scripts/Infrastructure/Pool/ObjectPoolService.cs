﻿using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.Utils.CustomDebug;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.Pool
{
	[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ObjectPoolService : IObjectPoolService, IDisposable
    {
	    private Transform _root;
	    private readonly Transform _parent;
	    private bool _logStatus;
	    private bool _dirty;
	    private List<ObjectPoolItem> _poolItems;
	    private List<ObjectPoolContainer<GameObject>> _release;
	    private IDictionary<GameObject, ObjectPool<GameObject>> _prefabLookup;
	    private IDictionary<GameObject, ObjectPool<GameObject>> _instanceLookup;
	    private readonly IAssetService _assetService;
	    private readonly IStaticDataService _staticDataService;
	    
	    public ObjectPoolService(IAssetService assetService, IStaticDataService staticDataService, Transform parent)
	    {
		    _assetService = assetService;
		    _staticDataService = staticDataService;
		    _parent = parent;
	    }

	    async UniTask IObjectPoolService.Init()
	    {
		    _root = new GameObject().transform;
		    _root.SetParent(_parent);
		    _root.name = "Pool";
		    
		    _prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
		    _instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
		    _release = new List<ObjectPoolContainer<GameObject>>();

		    await LoadPoolItems();
		    
		    FirstWarmPool();
	    }
	    
	    void IObjectPoolService.Execute()
	    {
		    if (_logStatus && _dirty)
		    {
			    PrintStatus();
			    
			    _dirty = false;
		    }

		    if (_release.Count > 0)
		    {
			    for (int i = _release.Count - 1; i >= 0; i--)
			    {
				    _release[i].Time -= Time.deltaTime;

				    if (_release[i].Time < 0f)
				    {
					    Release(_release[i].Item);
					    
					    _release.Remove(_release[i]);
				    }
			    }
		    }
	    }

	    GameObject IObjectPoolService.SpawnObject(GameObject prefab)
	    {
		    return Spawn(prefab);
	    }

	    GameObject IObjectPoolService.SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
	    {
		    return Spawn(prefab, position, rotation);
	    }

	    void IObjectPoolService.ReleaseObject(GameObject clone)
	    {
		    Release(clone);
	    }

	    void IObjectPoolService.ReleaseObjectAfterTime(GameObject clone, float time)
	    {
		    ObjectPoolContainer<GameObject> container = _instanceLookup[clone].GetContainer(clone);
		    container.Time = time;
		    _release.Add(container);
	    }

	    void IObjectPoolService.ReleaseAll()
	    {
		    foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> keyValuePair in _instanceLookup)
		    {
			    keyValuePair.Key.SetActive(false);
			    keyValuePair.Value.ReleaseAll();
		    }
		    
		    _release.Clear();
		    _instanceLookup.Clear();
	    }

	    private async UniTask LoadPoolItems()
	    {
		    PoolData data = _staticDataService.PoolData();

		    _logStatus = data.LogStatus;
		    _poolItems = new List<ObjectPoolItem>(data.PoolItems.Length);

		    for (int i = 0; i < data.PoolItems.Length; i++)
		    {
			    int count = data.PoolItems[i].Count;

			    if (count > 0)
			    {
				    GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PoolItems[i].PrefabReference);

				    _poolItems.Add(new ObjectPoolItem(prefab, count));
			    }
		    }
	    }

	    private void Warm(GameObject prefab, int size)
	    {
		    if (_prefabLookup.ContainsKey(prefab))
		    {
			    CustomDebug.LogError($"Pool for prefab {prefab.name} has already been created");
		    }

		    ObjectPool<GameObject> pool = new ObjectPool<GameObject>(() => InstantiatePrefab(prefab), size);
		    
		    _prefabLookup[prefab] = pool;
		    
		    _dirty = true;
	    }

	    private GameObject Spawn(GameObject prefab)
	    {
		    return Spawn(prefab, Vector3.zero, Quaternion.identity);
	    }

	    private GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
	    {
		    if (!_prefabLookup.ContainsKey(prefab))
		    {
			    WarmPool(prefab, 1);
		    }

		    ObjectPool<GameObject> pool = _prefabLookup[prefab];

		    GameObject clone = pool.GetItem();
		    
		    clone.transform.position = position;
		    clone.transform.rotation = rotation;
		    clone.SetActive(true);

		    _instanceLookup.Add(clone, pool);
		    
		    _dirty = true;
		    
		    return clone;
	    }

	    private void Release(GameObject clone)
	    {
		    clone.SetActive(false);

		    if (_instanceLookup.ContainsKey(clone))
		    {
			    _instanceLookup[clone].ReleaseItem(clone);
			    _instanceLookup.Remove(clone);
			    _dirty = true;
		    }
		    else
		    {
			    CustomDebug.LogWarning($"No pool contains the object: {clone.name}");
		    }
	    }

	    private GameObject InstantiatePrefab(GameObject prefab)
	    {
		    GameObject go = UnityEngine.Object.Instantiate(prefab);
		    
		    go.SetActive(false);
		    
		    if (_root != null)
		    {
			    go.transform.parent = _root;
		    }

		    return go;
	    }

	    private void PrintStatus()
	    {
		    foreach (KeyValuePair<GameObject, ObjectPool<GameObject>> dictionary in _prefabLookup)
		    {
			    string message = $"Object Pool for Prefab: {dictionary.Key.name} In Use: {dictionary.Value.CountUsedItems.ToString()} Total: {dictionary.Value.Count.ToString()}";
			    
			    CustomDebug.Log(message, DebugColorType.Lime);
		    }
	    }

	    private void FirstWarmPool()
	    {
		    for (int i = 0; i < _poolItems.Count; i++)
		    {
			    WarmPool(_poolItems[i].Prefab, _poolItems[i].Count);
		    }
	    }

	    private void WarmPool(GameObject prefab, int size)
	    {
		    Warm(prefab, size);
	    }

	    void IDisposable.Dispose()
	    {
		    _prefabLookup.Clear();
		    _instanceLookup.Clear();
	    }
    }
}