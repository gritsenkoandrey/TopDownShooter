using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBullet : EntityComponent<CBullet> , IBullet
    {
        public Vector3 Position => transform.position;
        public Vector3 Direction { get; private set; }
        public float CollisionDistance { get; private set; }
        public void SetDirection(Vector3 direction) => Direction = direction;
        public void SetCollisionDistance(float collisionDistance) => CollisionDistance = collisionDistance;
        public int Damage { get; set; }
        public GameObject Object => gameObject;
        public ReactiveCommand OnDestroy { get; } = new();
    }
}