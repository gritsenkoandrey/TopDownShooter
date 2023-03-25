﻿using CodeBase.ECSCore;
using CodeBase.Infrastructure.Progress;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CHealth : EntityComponent<CHealth>, IProgressReader
    {
        [SerializeField] private Collider _collider;

        public Collider Collider => _collider;
        public int MaxHealth { get; set; }
        public int BaseHealth { get; set; }
        public ReactiveProperty<int> Health { get; } = new();

        public void Read(PlayerProgress progress)
        {
            MaxHealth = BaseHealth * progress.Stats.Health;
            Health.SetValueAndForceNotify(MaxHealth);
        }

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}