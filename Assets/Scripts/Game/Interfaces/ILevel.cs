namespace CodeBase.Game.Interfaces
{
    public interface ILevel : ILoot
    {
        public int Time { get; }
        public int MaxTime { get; }
        public void RemoveTime();
    }
}