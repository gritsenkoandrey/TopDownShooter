using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
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
        }

        private void CheckEnemyCollision(IBullet bullet)
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                bool isCollision = (bullet.Position - _levelModel.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

                if (isCollision)
                {
                    Collision(bullet, _levelModel.Enemies[i]).Forget();
                    AddLog(bullet, _levelModel.Enemies[i]);
                }
            }
        }

        private void CheckCharacterCollision(IBullet bullet)
        {
            bool isCollision = (bullet.Position - _levelModel.Character.Position).sqrMagnitude < bullet.CollisionDistance;

            if (isCollision)
            {
                Collision(bullet, _levelModel.Character).Forget();
            }
        }

        private async UniTaskVoid Collision(IBullet bullet, ITarget target)
        {
            await UniTask.Yield();

            target.Health.CurrentHealth.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
            
            _effectFactory.CreateHitFx(bullet.Object.transform.position).Forget();
        }

        private void AddLog(IBullet bullet, ITarget target)
        {
            _damageCombatLog.AddLog(target, bullet.Damage);
        }
    }
}