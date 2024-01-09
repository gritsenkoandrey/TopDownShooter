using System;
using System.Collections.Generic;
using CodeBase.ECSCore;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CDamageViewProvider : EntityComponent<CDamageViewProvider>
    {
        public IList<CDamageView> DamageViews = Array.Empty<CDamageView>();
    }
}