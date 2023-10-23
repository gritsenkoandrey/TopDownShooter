using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using Cysharp.Threading.Tasks;
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

        protected override void OnEnableComponent(CWeapon component)
        {
            base.OnEnableComponent(component);

            component.Shoot
                .Subscribe(_ => CreateBullet(component).Forget())
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CWeapon component)
        {
            base.OnDisableComponent(component);
        }

        private async UniTaskVoid CreateBullet(CWeapon component)
        {
            int damage = component.Damage;
            Vector3 position = component.SpawnBulletPoint.position;
            Vector3 direction = component.transform.forward.normalized * component.Force;
            
            await _gameFactory.CreateBullet(damage, position, direction);
        }
    }
}