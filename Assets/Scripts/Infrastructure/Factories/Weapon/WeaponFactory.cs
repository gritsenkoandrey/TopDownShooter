﻿using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Factories.Weapon
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class WeaponFactory : IWeaponFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetService _assetService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly IObjectResolver _objectResolver;

        public WeaponFactory(IStaticDataService staticDataService, IAssetService assetService, 
            IObjectPoolService objectPoolService, IObjectResolver objectResolver)
        {
            _staticDataService = staticDataService;
            _assetService = assetService;
            _objectPoolService = objectPoolService;
            _objectResolver = objectResolver;
        }

        async UniTask<CWeapon> IWeaponFactory.CreateCharacterWeapon(WeaponType type, Transform parent)
        {
            WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CWeapon weapon = Object.Instantiate(prefab, parent).GetComponent<CWeapon>();
            weapon.SetWeapon(CreateSpecificCharacterWeapon(type, weapon, data.WeaponCharacteristic));
            return weapon;
        }

        async UniTask<CWeapon> IWeaponFactory.CreateUnitWeapon(WeaponType type, WeaponCharacteristic weaponCharacteristic, Transform parent)
        {
            WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CWeapon weapon = Object.Instantiate(prefab, parent).GetComponent<CWeapon>();
            weapon.SetWeapon(CreateSpecificUnitWeapon(type, weapon, weaponCharacteristic));
            return weapon;
        }

        IWeapon IWeaponFactory.CreateTurretWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic)
        {
            BaseWeapon currentWeapon = new UnitRangeWeapon(weapon, weaponCharacteristic);
            _objectResolver.Inject(currentWeapon);
            currentWeapon.Initialize();
            return currentWeapon;
        }

        async UniTask<IProjectile> IWeaponFactory.CreateProjectile(ProjectileType type, Transform spawnPoint, int damage, Vector3 direction)
        {
            ProjectileData data = _staticDataService.ProjectileData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CBullet bullet = _objectPoolService.SpawnObject(prefab, spawnPoint.position, spawnPoint.rotation).GetComponent<CBullet>();
            bullet.LifeTime = data.LifeTime;
            bullet.SetDamage(damage);
            bullet.SetDirection(direction);
            bullet.SetCollisionDistance(Mathf.Pow(data.CollisionRadius, 2));
            return bullet;
        }

        private IWeapon CreateSpecificCharacterWeapon(WeaponType type, CWeapon weapon, WeaponCharacteristic weaponCharacteristic)
        {
            BaseWeapon currentWeapon = type == WeaponType.Knife
                ? new CharacterMeleeWeapon(weapon, weaponCharacteristic)
                : new CharacterRangeWeapon(weapon, weaponCharacteristic);
            _objectResolver.Inject(currentWeapon);
            currentWeapon.Initialize();
            return currentWeapon;
        }

        private IWeapon CreateSpecificUnitWeapon(WeaponType type, CWeapon weapon, WeaponCharacteristic weaponCharacteristic)
        {
            BaseWeapon currentWeapon = type == WeaponType.Knife
                ? new UnitMeleeWeapon(weapon, weaponCharacteristic)
                : new UnitRangeWeapon(weapon, weaponCharacteristic);
            _objectResolver.Inject(currentWeapon);
            currentWeapon.Initialize();
            return currentWeapon;
        }
    }
}