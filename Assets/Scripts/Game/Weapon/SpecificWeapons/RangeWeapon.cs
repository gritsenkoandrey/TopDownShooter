using System;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class RangeWeapon : BaseWeapon, IWeapon
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly IProgressService _progressService;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly CWeapon _weapon;
        private readonly InventoryModel _inventoryModel;

        private int _clipCount;
        private float _attackDistance;
        private bool _canAttack;

        private Tween _speedAttackTween;
        private Tween _rechargeTimeTween;

        public RangeWeapon(CWeapon weapon, IWeaponFactory weaponFactory, IProgressService progressService, 
            WeaponCharacteristic weaponCharacteristic, InventoryModel inventoryModel) 
            : base(weaponFactory, progressService)
        {
            _weapon = weapon;
            _weaponFactory = weaponFactory;
            _progressService = progressService;
            _weaponCharacteristic = weaponCharacteristic;
            _inventoryModel = inventoryModel;

            SetAttackDistance();
            SetClipCount();
            SetCanAttack();
        }
        
        void IWeapon.Attack()
        {
            CreateBullet().Forget();

            _canAttack = false;

            _speedAttackTween?.Kill();
            _speedAttackTween = DOVirtual.DelayedCall(_weaponCharacteristic.SpeedAttack, SetCanAttack);
            
            _clipCount--;

            _inventoryModel.ClipCount.Value = _clipCount;

            if (_clipCount <= 0)
            {
                _rechargeTimeTween?.Kill();
                _rechargeTimeTween = DOVirtual.DelayedCall(_weaponCharacteristic.RechargeTime, SetClipCount);
            }
        }

        bool IWeapon.CanAttack() => _clipCount > 0 && _canAttack;

        bool IWeapon.IsDetectThroughObstacle() => _weaponCharacteristic.IsDetectThroughObstacle;

        float IWeapon.AttackDistance() => _attackDistance;

        private async UniTaskVoid CreateBullet()
        {
            int damage = _weaponCharacteristic.Damage * _progressService.PlayerProgress.Stats.Damage;
            Vector3 position = _weapon.SpawnBulletPointPosition;
            Vector3 direction = _weapon.NormalizeForwardDirection * _weaponCharacteristic.ForceBullet;
            
            await _weaponFactory.CreateBullet(damage, position, direction);
        }

        private void SetClipCount()
        {
            _clipCount = _weaponCharacteristic.ClipCount;
            _inventoryModel.ClipCount.Value = _clipCount;
        }

        private void SetCanAttack() => _canAttack = true;
        private void SetAttackDistance() => _attackDistance = Mathf.Pow(_weaponCharacteristic.AttackDistance, 2);
        
        void IDisposable.Dispose()
        {
            _speedAttackTween?.Kill();
            _rechargeTimeTween?.Kill();
        }
    }
}