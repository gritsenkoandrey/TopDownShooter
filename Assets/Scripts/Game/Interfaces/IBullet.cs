using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IBullet : IObject, IPosition
    {
        public void SetDirection(Vector3 direction);
        public int Damage { get; set; }
        public ReactiveCommand OnDestroy { get; }
    }
}