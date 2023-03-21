using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterWeapon : SystemComponent<CWeapon>
    {
        private readonly IGameFactory _gameFactory;

        public SCharacterWeapon(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
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