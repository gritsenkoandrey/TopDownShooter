using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CHealth : EntityComponent<CHealth>
    {
        [SerializeField] private Collider _collider;

        public Collider Collider => _collider;
        public int MaxHealth { get; set; }
        public ReactiveProperty<int> Health { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}