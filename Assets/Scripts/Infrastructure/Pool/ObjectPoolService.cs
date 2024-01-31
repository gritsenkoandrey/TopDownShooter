using System;
using System.Collections.Generic;
using System.Threading;
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
	    private IDictionary<GameObject, ObjectPool<GameObject>> _prefabLookup;
	    private IDictionary<GameObject, ObjectPool<GameObject>> _instanceLookup;
	    private CancellationToken _token;
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
		    _token = new CancellationToken();

		    await LoadPoolItems();
		    
		    FirstWarmPool();
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

	    async UniTaskVoid IObjectPoolService.ReleaseObjectAfterTime(GameObject clone, float time)
	    {
		    try
		    {
			    await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _token);
			    
			    Release(clone);
		    }
		    catch (OperationCanceledException exception)
		    {
			    if (exception.CancellationToken == _token)
			    {
				    CustomDebug.LogWarning($"{exception.CancellationToken}");
			    }
		    }
	    }

	    void IObjectPoolService.Log()
	    {
		    if (_logStatus && _dirty)
		    {
			    PrintStatus();
			    
			    _dirty = false;
		    }
	    }

	    private async UniTask LoadPoolItems()
	    {
		    PoolData data = _staticDataService.PoolData();

		    _logStatus = data.LogStatus;
		    _poolItems = new List<ObjectPoolItem>(data.PoolItems.Length);

		    for (int i = 0; i < data.PoolItems.Length; i++)
		    {
			    int count = data.PoolItems[i].Count;
			    GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PoolItems[i].PrefabReference);

			    _poolItems.Add(new ObjectPoolItem(prefab, count));
		    }
	    }

	    private void Warm(GameObject prefab, int size)
	    {
		    prefab.gameObject.SetActive(false);

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
			    string message = $"Object Pool for Prefab: {dictionary.Key.name} In Use: {dictionary.Value.CountUsedItems} Total: {dictionary.Value.Count}";
			    
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

	    private void SetActivePoolPrefabs()
	    {
		    for (int i = 0; i < _poolItems.Count; i++)
		    {
			    _poolItems[i].Prefab.SetActive(true);
		    }
	    }

	    void IDisposable.Dispose()
	    {
		    _prefabLookup.Clear();
		    _instanceLookup.Clear();
		    _token.ThrowIfCancellationRequested();

		    SetActivePoolPrefabs();
	    }
    }
}