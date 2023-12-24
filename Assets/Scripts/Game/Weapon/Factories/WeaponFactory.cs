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
using UnityEngine;

namespace CodeBase.Game.Weapon.Factories
{
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

            return new WeaponBuilder(this, _progressService)
                .SetPrefab(prefab)
                .SetParent(parent)
                .SetWeaponType(type)
                .SetWeaponCharacteristic(data.WeaponCharacteristic)
                .SetInventoryModel(_inventoryModel)
                .Build();
        }

        async UniTask<IBullet> IWeaponFactory.CreateBullet(int damage, Vector3 position, Vector3 direction)
        {
            BulletData data = _staticDataService.BulletData();
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(AssetAddress.Bullet);

            return new BulletBuilder(_objectPoolService)
                .SetPrefab(prefab)
                .SetDamage(damage)
                .SetPosition(position)
                .SetDirection(direction)
                .SetCollisionDistance(data.CollisionRadius)
                .Build();
        }
    }
}