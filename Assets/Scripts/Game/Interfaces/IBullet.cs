using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IBullet : IObject, IPosition
    {
        Vector3 Direction { get; }
        float CollisionDistance { get; }
        int Damage { get; }
        ReactiveCommand OnDestroy { get; }
    }
}