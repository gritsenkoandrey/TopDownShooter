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
        
        async UniTask<GameObject> IEffectFactory.CreateHitFx(Vector3 position)
        {
            FxData data = _staticDataService.FxData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.HitFx);

            GameObject hitFx = _objectPoolService.SpawnObject(prefab, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(hitFx, data.HitFxReleaseTime).Forget();
            
            return hitFx;
        }

        async UniTask<GameObject> IEffectFactory.CreateDeathFx(Vector3 position)
        {
            FxData data = _staticDataService.FxData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.DeathFx);

            GameObject deathFx = _objectPoolService.SpawnObject(prefab, position, Quaternion.identity);
            
            _objectPoolService.ReleaseObjectAfterTime(deathFx, data.DeathFxReleaseTime).Forget();

            return deathFx;
        }
    }
}