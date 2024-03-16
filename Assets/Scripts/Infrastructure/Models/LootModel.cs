using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Progress;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class LootModel
    {
        private readonly IProgressService _progressService;

        private const float Multiplier = 0.1f;

        public LootModel(IProgressService progressService)
        {
            _progressService = progressService;
        }
        
        public int GenerateLevelLoot(ILevel level)
        {
            int loot = level.Loot + Mathf.RoundToInt(GetLevelIndex() * Multiplier * level.Loot);

            AddLoot(loot);
            
            return loot;
        }

        public int GenerateEnemyLoot(IEnemy enemy)
        {
            int loot = enemy.Loot + Mathf.RoundToInt(GetLevelIndex() * Multiplier * enemy.Loot);
            
            AddLoot(loot);

            return loot;
        }

        private int GetLevelIndex() => _progressService.LevelData.Data.Value;
        
        private void AddLoot(int loot) => _progressService.MoneyData.Data.Value += loot;
    }
}