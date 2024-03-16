using System;
using System.Collections.Generic;
using CodeBase.ECSCore;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CMoneyLootProvider : EntityComponent<CMoneyLootProvider>
    {
        public IList<CMoneyLoot> MoneyLoots = Array.Empty<CMoneyLoot>();
    }
}