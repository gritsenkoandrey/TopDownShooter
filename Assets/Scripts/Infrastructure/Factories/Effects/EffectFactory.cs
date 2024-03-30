using CodeBase.Game.Enums;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Effects
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class EffectFactory : IEffectFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly IAssetService _assetService;

        public EffectFactory(
            IStaticDataService staticDataService, 
            IObjectPoolService objectPoolService, 
            IAssetService assetService)
        {
            _staticDataService = staticDataService;
            _objectPoolService = objectPoolService;
            _assetService = assetService;
        }
        
        async UniTask<GameObject> IEffectFactory.CreateEffect(EffectType type, Vector3 position)
        {
            EffectData data = _staticDataService.EffectData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            GameObject effect = _objectPoolService.SpawnObject(prefab, position, prefab.transform.rotation);
            _objectPoolService.ReleaseObjectAfterTime(effect, data.LifeTime).Forget();
            return effect;
        }
    }
}