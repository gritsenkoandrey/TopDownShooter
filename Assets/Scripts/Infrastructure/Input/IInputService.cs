using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public interface IInputService : IService
    {
        public Vector2 Value { get; }
    }
}