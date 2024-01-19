using System;
using System.Collections.Generic;
using CodeBase.ECSCore;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CDamageCombatLogViewProvider : EntityComponent<CDamageCombatLogViewProvider>
    {
        public IList<CDamageCombatLogView> DamageCombatLogViews = Array.Empty<CDamageCombatLogView>();
    }
}