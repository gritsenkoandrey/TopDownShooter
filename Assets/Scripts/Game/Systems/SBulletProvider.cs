using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletProvider : SystemComponent<CBullet>
    {
        private readonly IEffectFactory _effectFactory;
        private readonly LevelModel _levelModel;

        public SBulletProvider(IEffectFactory effectFactory, LevelModel levelModel)
        {
            _effectFactory = effectFactory;
            _levelModel = levelModel;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            foreach (CBullet bullet in Entities)
            {
                Move(bullet);
                
                if (CheckCollision(bullet))
                {
                    return;
                }
            }
        }

        private void Move(IBullet bullet)
        {
            bullet.Object.transform.position += bullet.Direction;
        }

        private bool CheckCollision(IBullet bullet)
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                bool isCollision = (bullet.Position - _levelModel.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

                if (isCollision)
                {
                    Collision(bullet, _levelModel.Enemies[i]);
                        
                    return true;
                }
            }

            return false;
        }

        private void Collision(IBullet bullet, ITarget target)
        {
            target.Health.CurrentHealth.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
                
            _effectFactory.CreateHitFx(bullet.Object.transform.position);
        }
    }
}