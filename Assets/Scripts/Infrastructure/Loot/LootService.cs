using System;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.Loot
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LootService : ILootService
    {
        private readonly IProgressService _progressService;

        private const float Multiplier = 0.1f;
        
        public event Action<(ITarget, int)> OnAddLoot;

        public LootService(IProgressService progressService)
        {
            _progressService = progressService;
        }
        
        int ILootService.GenerateLevelLoot(ILevel level)
        {
            int loot = level.Loot + Mathf.RoundToInt(GetLevelIndex() * Multiplier * level.Loot);

            AddLoot(loot);
            
            return loot;
        }

        int ILootService.GenerateEnemyLoot(IEnemy enemy)
        {
            int loot = enemy.Loot + Mathf.RoundToInt(GetLevelIndex() * Multiplier * enemy.Loot);
            
            AddLoot(loot);

            OnAddLoot?.Invoke((enemy, loot));

            return loot;
        }

        private int GetLevelIndex() => _progressService.LevelData.Data.Value;
        
        private void AddLoot(int loot) => _progressService.MoneyData.Data.Value += loot;
    }
}