using System;
using System.Collections.Generic;
using CodeBase.ECSCore;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CEnemyHealthProvider : EntityComponent<CEnemyHealthProvider>
    {
        public IList<CEnemyHealth> EnemyHealths = Array.Empty<CEnemyHealth>();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}