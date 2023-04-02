using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IBullet : IObject
    {
        public Rigidbody Rigidbody { get; }
        public int Damage { get; set; }
        public ReactiveCommand OnDestroy { get; }
    }
}