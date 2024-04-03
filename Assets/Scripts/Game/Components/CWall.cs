using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWall : EntityComponent<CWall>, IObstacle
    {
        [SerializeField] private BoxCollider _boxCollider;

        public Bounds Bounds => _boxCollider.bounds;
    }
}