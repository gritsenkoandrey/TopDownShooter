using CodeBase.Game.Enums;

namespace CodeBase.Game.Interfaces
{
    public interface ISpawnPoint : IPosition
    {
        public UnitType UnitType { get; }
        public ZombieType ZombieType { get; }
    }
}