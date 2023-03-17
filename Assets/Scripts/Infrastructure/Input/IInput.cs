using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.Input
{
    public interface IInput : IObject
    {
        public Vector2 Value { get; set; }
    }
}