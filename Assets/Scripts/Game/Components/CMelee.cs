using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    [RequireComponent(typeof(Animator))]
    public sealed class CMelee : EntityComponent<CMelee>
    {
        public int Damage { get; set; }
        public ReactiveCommand Attack { get; } = new();
        public ReactiveCommand OnCheckDamage { get; } = new();
        public void OnAttack() => OnCheckDamage.Execute();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}