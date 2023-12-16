using CodeBase.Game.Components;
using CodeBase.Game.Models;

namespace CodeBase.Game.Interfaces
{
    public interface ITarget : IPosition
    {
        public Health Health { get; }
    }
}