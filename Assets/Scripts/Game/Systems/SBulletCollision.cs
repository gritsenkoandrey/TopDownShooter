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
        private readonly DamageCombatLog _damageCombatLog;
        
        public SBulletCollision(IEffectFactory effectFactory, LevelModel levelModel, DamageCombatLog damageCombatLog)
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
                    EnemyCollision(bullet, _levelModel.Enemies[i]).Forget();
                }
            }
        }

        private void CheckCharacterCollision(IBullet bullet)
        {
            bool isCollision = (bullet.Position - _levelModel.Character.Move.Position).sqrMagnitude < bullet.CollisionDistance;

            if (isCollision)
            {
                CharacterCollision(bullet, _levelModel.Character).Forget();
            }
        }

        private async UniTaskVoid EnemyCollision(IBullet bullet, IEnemy target)
        {
            await UniTask.Yield();

            target.Health.CurrentHealth.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
               
            _damageCombatLog.AddLog(target, bullet.Damage);
            _effectFactory.CreateHitFx(bullet.Object.transform.position).Forget();
        }
        
        private async UniTaskVoid CharacterCollision(IBullet bullet, ICharacter character)
        {
            await UniTask.Yield();
            
            character.Health.CurrentHealth.Value -= bullet.Damage;
            bullet.OnDestroy.Execute();
               
            _effectFactory.CreateHitFx(bullet.Object.transform.position).Forget();
        }
    }
}