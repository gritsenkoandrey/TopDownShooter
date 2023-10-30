using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IBullet : IObject, IPosition
    {
        public void SetDirection(Vector3 direction);
        public void SetCollisionDistance(float collisionDistance);
        public Vector3 Direction { get; }
        public float CollisionDistance { get; }
        public int Damage { get; set; }
        public ReactiveCommand OnDestroy { get; }
    }
}