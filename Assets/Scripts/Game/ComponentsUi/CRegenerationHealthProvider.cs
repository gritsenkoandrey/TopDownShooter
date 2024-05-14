using System;
using System.Collections.Generic;
using CodeBase.ECSCore;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CRegenerationHealthProvider : EntityComponent<CRegenerationHealthProvider>
    {
        public IList<CRegenerationHealth> Elements = Array.Empty<CRegenerationHealth>();
    }
}