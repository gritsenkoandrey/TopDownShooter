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
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
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

        protected override void OnEnableComponent(CBullet component)
        {
            base.OnEnableComponent(component);
        }

        protected override void OnDisableComponent(CBullet component)
        {
            base.OnDisableComponent(component);
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
            target.Health.Health.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
                
            _gameFactory.CreateHitFx(bullet.Object.transform.position);
        }
    }
}