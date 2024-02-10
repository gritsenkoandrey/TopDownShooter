﻿using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Models;
using CodeBase.UI.Screens;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletCollision : SystemComponent<CBullet>
    {
        private IEffectFactory _effectFactory;
        private IGuiService _guiService;
        private LevelModel _levelModel;
        private DamageCombatLog _damageCombatLog;
        
        [Inject]
        private void Construct(IEffectFactory effectFactory, IGuiService guiService, LevelModel levelModel, DamageCombatLog damageCombatLog)
        {
            _effectFactory = effectFactory;
            _guiService = guiService;
            _levelModel = levelModel;
            _damageCombatLog = damageCombatLog;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            if (_guiService.StaticCanvas.CurrentScreenType != ScreenType.Game) return;
            
            Entities.Foreach(CheckEnemyCollision);
            Entities.Foreach(CheckCharacterCollision);
        }

        private void CheckEnemyCollision(IBullet bullet)
        {
            for (int i = 0; i < _levelModel.Enemies.Count; i++)
            {
                bool targetIsAlive = _levelModel.Enemies[i].Health.IsAlive;
                bool isCollision = (bullet.Position - _levelModel.Enemies[i].Position).sqrMagnitude < bullet.CollisionDistance;

                if (targetIsAlive && isCollision)
                {
                    Collision(bullet, _levelModel.Enemies[i]);
                    AddLog(bullet, _levelModel.Enemies[i]);
                }
            }
        }

        private void CheckCharacterCollision(IBullet bullet)
        {
            bool targetIsAlive = _levelModel.Character.Health.IsAlive;
            bool isCollision = (bullet.Position - _levelModel.Character.Position).sqrMagnitude < bullet.CollisionDistance;

            if (targetIsAlive && isCollision)
            {
                Collision(bullet, _levelModel.Character);
            }
        }

        private void Collision(IBullet bullet, ITarget target)
        {
            target.Health.CurrentHealth.Value -= bullet.Damage;

            bullet.OnDestroy.Execute();
            
            _effectFactory.CreateEffect(EffectType.Hit, target.Position).Forget();
        }

        private void AddLog(IBullet bullet, ITarget target) => _damageCombatLog.AddLog(target, bullet.Damage);
    }
}