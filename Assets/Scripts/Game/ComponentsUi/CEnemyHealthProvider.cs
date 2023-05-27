using System.Collections.Generic;
using CodeBase.ECSCore;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CEnemyHealthProvider : EntityComponent<CEnemyHealthProvider>
    {
        public readonly List<CEnemyHealth> EnemyHealths = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}