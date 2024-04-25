using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBullet : EntityComponent<CBullet> , IProjectile
    {
        public Vector3 Position => transform.position;
        public Vector3 Direction { get; private set; }
        public float CollisionDistance { get; private set; }
        public int Damage { get; private set; }
        public float LifeTime { get; set; } = int.MaxValue;
        
        public void SetDirection(Vector3 direction) => Direction = direction;
        public void SetCollisionDistance(float collisionDistance) => CollisionDistance = collisionDistance;
        public void SetDamage(int damage) => Damage = damage;
        
        public IReactiveCommand<Unit> OnDestroy { get; } = new ReactiveCommand();
    }
}