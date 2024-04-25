using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletCollision : SystemComponent<CBullet>
    {
        private IEffectFactory _effectFactory;
        private LevelModel _levelModel;
        private DamageCombatLog _damageCombatLog;
        
        [Inject]
        private void Construct(IEffectFactory effectFactory, LevelModel levelModel, DamageCombatLog damageCombatLog)
        {
            _effectFactory = effectFactory;
            _levelModel = levelModel;
            _damageCombatLog = damageCombatLog;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(CheckEnemyCollision);
            Entities.Foreach(CheckCharacterCollision);
            Entities.Foreach(CheckObstacleCollision);
        }

        private void CheckEnemyCollision(CBullet bullet)
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                bool targetIsAlive = _levelModel.Enemies[i].Health.IsAlive;
                bool isCollision = (bullet.Position - _levelModel.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

                if (targetIsAlive && isCollision)
                {
                    AddLog(bullet, _levelModel.Enemies[i]);
                    Collision(bullet, _levelModel.Enemies[i]);
                }
            }
        }

        private void CheckCharacterCollision(CBullet bullet)
        {
            bool targetIsAlive = _levelModel.Character.Health.IsAlive;
            bool isCollision = (bullet.Position - _levelModel.Character.Position).sqrMagnitude < bullet.CollisionDistance;

            if (targetIsAlive && isCollision)
            {
                Collision(bullet, _levelModel.Character);
            }
        }

        private void Collision(CBullet bullet, ITarget target)
        {
            target.Health.CurrentHealth.Value -= bullet.Damage;

            bullet.OnDestroy.Execute(Unit.Default);

            switch (target)
            {
                case CCharacter:
                case CUnit:
                    _effectFactory.CreateEffect(EffectType.Hit, target.Position).Forget();
                    break;
                case CTurret:
                    _effectFactory.CreateEffect(EffectType.Explosion, bullet.Position).Forget();
                    break;
            }
        }

        private void CheckObstacleCollision(CBullet bullet)
        {
            for (int i = 0; i < _levelModel.Obstacles.Count; i++)
            {
                bool isCollision = _levelModel.Obstacles[i].Bounds.Contains(bullet.Position);

                if (isCollision)
                {
                    bullet.OnDestroy.Execute(Unit.Default);
                    
                    _effectFactory.CreateEffect(EffectType.Explosion, bullet.Position).Forget();
                }
            }
        }

        private void AddLog(CBullet bullet, ITarget target) => _damageCombatLog.AddLog(target, bullet.Damage);
    }
}