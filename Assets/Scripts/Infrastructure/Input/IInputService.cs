using AndreyGritsenko.Infrastructure.Services;
using UnityEngine;

namespace AndreyGritsenko.Infrastructure.Input
{
    public interface IInputService : IService
    {
        public Vector2 Value { get; }
    }
}