using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IBullet : IObject, IPosition
    {
        public Vector3 Direction { get; }
        public float CollisionDistance { get; }
        public int Damage { get; }
        public ReactiveCommand OnDestroy { get; }
    }
}