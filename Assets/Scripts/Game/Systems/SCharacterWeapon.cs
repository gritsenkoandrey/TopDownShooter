using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterWeapon : SystemComponent<CWeapon>
    {
        private IGameFactory _gameFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CWeapon weapon)
        {
            base.OnEnableComponent(weapon);

            weapon.Shoot
                .Subscribe(_ =>
                {
                    IBullet bullet = _gameFactory.CreateBullet(weapon.SpawnBulletPoint.position);

                    bullet.Damage = weapon.Damage;
                    bullet.Rigidbody.AddForce(weapon.transform.forward * weapon.Force, ForceMode.Impulse);
                })
                .AddTo(weapon.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CWeapon component)
        {
            base.OnDisableComponent(component);
        }
    }
}