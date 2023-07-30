using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IBullet : IObject, IPosition
    {
        public Vector3 Direction { get; set; }
        public int Damage { get; set; }
        public ReactiveCommand OnDestroy { get; }
    }
}