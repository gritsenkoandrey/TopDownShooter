using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Game.Weapon.Factories
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class WeaponFactory : IWeaponFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetService _assetService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly IProgressService _progressService;
        private readonly InventoryModel _inventoryModel;

        public WeaponFactory(
            IStaticDataService staticDataService, 
            IAssetService assetService, 
            IObjectPoolService objectPoolService, 
            IProgressService progressService,
            InventoryModel inventoryModel)
        {
            _staticDataService = staticDataService;
            _assetService = assetService;
            _objectPoolService = objectPoolService;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
        }

        async UniTask<CWeapon> IWeaponFactory.CreateWeapon(WeaponType type, Transform parent)
        {
            WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            return new WeaponBuilder(this, _progressService, data.WeaponCharacteristic, _inventoryModel)
                .SetPrefab(prefab)
                .SetParent(parent)
                .SetWeaponType(type)
                .Build();
        }

        async UniTask<IBullet> IWeaponFactory.CreateProjectile(ProjectileType type, Transform spawnPoint, int damage, Vector3 direction)
        {
            ProjectileData data = _staticDataService.ProjectileData(type);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            return new BulletBuilder(_objectPoolService)
                .SetPrefab(prefab)
                .SetSpawnPoint(spawnPoint)
                .SetDamage(damage)
                .SetDirection(direction)
                .SetCollisionDistance(data.CollisionRadius)
                .Build();
        }
    }
}