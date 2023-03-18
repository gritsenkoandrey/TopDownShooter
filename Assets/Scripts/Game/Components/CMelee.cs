using CodeBase.ECSCore;
using UniRx;

namespace CodeBase.Game.Components
{
    public sealed class CMelee : EntityComponent<CMelee>
    {
        public int Damage { get; set; }
        public ReactiveCommand Attack { get; } = new();
        public ReactiveCommand OnAttack { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}