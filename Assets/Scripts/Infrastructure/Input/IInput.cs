using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public interface IInput
    {
        public Vector2 Vector { get; }
        public void OnUpdate();
    }
}