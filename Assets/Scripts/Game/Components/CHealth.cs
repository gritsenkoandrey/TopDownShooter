using CodeBase.ECSCore;
using UniRx;

namespace CodeBase.Game.Components
{
    public sealed class CHealth : EntityComponent<CHealth>
    {
        public int Health { get; set; }

        public ReactiveCommand Hit { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}