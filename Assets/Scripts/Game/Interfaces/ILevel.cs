namespace CodeBase.Game.Interfaces
{
    public interface ILevel : ILoot
    {
        int Time { get; }
        int MaxTime { get; }
        void RemoveTime();
    }
}