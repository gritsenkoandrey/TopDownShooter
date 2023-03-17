using CodeBase.Game.Components;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface ICharacter : IHealth, IObject, IPosition
    {
        public Vector2 Input { get; set; }
        public ReactiveCollection<CEnemy> Enemies { get; }
    }
}