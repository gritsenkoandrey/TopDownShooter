using System.Linq;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Game;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletProvider : SystemComponent<CBullet>
    {
        private readonly IGameFactory _gameFactory;

        public SBulletProvider(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
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
            for (int i = 0; i < _gameFactory.Enemies.Count; i++)
            {
                bool isCollision = (bullet.Position - _gameFactory.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

                if (isCollision)
                {
                    Collision(bullet, _gameFactory.Enemies[i]);
                        
                    return true;
                }
            }

            return false;
        }

        private void Collision(IBullet bullet, ITarget target)
        {
            target.Health.CurrentHealth.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
                
            _gameFactory.CreateHitFx(bullet.Object.transform.position);
        }
    }
}