using CodeBase.Game.Builders.Projectile;
using CodeBase.Game.Builders.Weapon;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Weapon
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class WeaponFactory : IWeaponFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetService _assetService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly IProgressService _progressService;
        private readonly IEffectFactory _effectFactory;
        private readonly InventoryModel _inventoryModel;
        private readonly DamageCombatLog _damageCombatLog;

        public WeaponFactory(
            IStaticDataService staticDataService, 
            IAssetService assetService, 
            IObjectPoolService objectPoolService,
            IProgressService progressService,
            IEffectFactory effectFactory,
            InventoryModel inventoryModel,
            DamageCombatLog damageCombatLog)
        {
            _staticDataService = staticDataService;
            _assetService = assetService;
            _objectPoolService = objectPoolService;
            _progressService = progressService;
            _effectFactory = effectFactory;
            _inventoryModel = inventoryModel;
            _damageCombatLog = damageCombatLog;
        }

        async UniTask<CWeapon> IWeaponFactory.CreateCharacterWeapon(WeaponType type, Transform parent)
        {
            WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            return new WeaponCharacterBuilder(this, data.WeaponCharacteristic, _damageCombatLog, _progressService, _inventoryModel, _effectFactory)
                .SetPrefab(prefab)
                .SetParent(parent)
                .SetWeaponType(type)
                .Build();
        }

        async UniTask<CWeapon> IWeaponFactory.CreateUnitWeapon(WeaponType type, WeaponCharacteristic weaponCharacteristic, Transform parent)
        {
            WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            return new WeaponUnitBuilder(this, weaponCharacteristic, _effectFactory)
                .SetPrefab(prefab)
                .SetParent(parent)
                .SetWeaponType(type)
                .Build();
        }

        async UniTask<IBullet> IWeaponFactory.CreateProjectile(ProjectileType type, Transform spawnPoint, int damage, Vector3 direction)
        {
            ProjectileData data = _staticDataService.ProjectileData(type);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            return new ProjectileBuilder(_objectPoolService)
                .SetPrefab(prefab)
                .SetData(data)
                .SetSpawnPoint(spawnPoint)
                .SetDamage(damage)
                .SetDirection(direction)
                .Build();
        }
    }
}