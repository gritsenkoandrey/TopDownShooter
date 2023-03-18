using CodeBase.Game.Components;
using CodeBase.Infrastructure.Input;
using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface ICharacter : IHealth, IObject, IPosition, IInput
    {
        public ReactiveCollection<CEnemy> Enemies { get; }
    }
}