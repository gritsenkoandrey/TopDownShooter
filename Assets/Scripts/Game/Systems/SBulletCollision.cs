using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletCollision : SystemComponent<CBullet>
    {
        private readonly IEffectFactory _effectFactory;
        private readonly LevelModel _levelModel;

        public SBulletCollision(IEffectFactory effectFactory, LevelModel levelModel)
        {
            _effectFactory = effectFactory;
            _levelModel = levelModel;
        }

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Entities.Foreach(CheckCollision);
        }

        private void CheckCollision(IBullet bullet)
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                bool isCollision = (bullet.Position - _levelModel.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

                if (isCollision)
                {
                    Collision(bullet, _levelModel.Enemies[i]).Forget();
                }
            }
        }

        private async UniTaskVoid Collision(IBullet bullet, ITarget target)
        {
            await UniTask.Yield();
            
            target.Health.CurrentHealth.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
                
            _effectFactory.CreateHitFx(bullet.Object.transform.position);
        }
    }
}