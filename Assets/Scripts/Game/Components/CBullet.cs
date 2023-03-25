﻿using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBullet : EntityComponent<CBullet> , IBullet
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;
        public int Damage { get; set; }
        public GameObject Object => gameObject;
        public Tween Tween { get; set; }

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}