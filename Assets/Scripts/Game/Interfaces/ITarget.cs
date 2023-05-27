using CodeBase.Game.Components;

namespace CodeBase.Game.Interfaces
{
    public interface ITarget : IPosition
    {
        public CHealth Health { get; }
    }
}