using System;
using CodeBase.Game.Interfaces;

namespace CodeBase.Infrastructure.Loot
{
    public interface ILootService
    {
        event Action<(ITarget, int)> OnAddLoot;
        int GenerateLevelLoot(ILevel level);
        int GenerateEnemyLoot(IEnemy enemy);
    }
}