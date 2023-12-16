using CodeBase.Game.Builders;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.AssetData;
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

        public WeaponFactory(
            IStaticDataService staticDataService, 
            IAssetService assetService, 
            IObjectPoolService objectPoolService, 
            IProgressService progressService)
        {
            _staticDataService = staticDataService;
            _assetService = assetService;
            _objectPoolService = objectPoolService;
            _progressService = progressService;
        }

        BaseWeapon IWeaponFactory.GetWeapon(CWeapon weapon, WeaponType type)
        {
            return new RangeWeapon(weapon, this, _progressService, type);
        }

        WeaponCharacteristic IWeaponFactory.GetWeaponCharacteristic(WeaponType type)
        {
            return _staticDataService.WeaponCharacteristicData(type).WeaponCharacteristic;
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