using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBullet : EntityComponent<CBullet> , IBullet
    {
        [SerializeField] private float _collisionDistance = 1f;
        
        public Vector3 Direction { get; private set; }
        public Vector3 Position => transform.position;
        public float CollisionDistance => _collisionDistance;
        public void SetDirection(Vector3 direction) => Direction = direction;
        public int Damage { get; set; }
        public GameObject Object => gameObject;
        public Tween Tween { get; set; }
        public ReactiveCommand OnDestroy { get; } = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}