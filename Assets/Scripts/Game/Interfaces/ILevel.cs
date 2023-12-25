using CodeBase.Game.Enums;

namespace CodeBase.Game.Interfaces
{
    public interface ILevel : IEntity
    {
        public LevelType LevelType { get; }
        public int LevelTime { get; }
    }
}