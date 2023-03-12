using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;

namespace CodeBase.Game.Components
{
    public sealed class CAttack : EntityComponent<CAttack>
    {
        public int Damage { get; set; }
        
        public ReactiveCommand<IHealth> Attack { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}