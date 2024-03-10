namespace CodeBase.Game.Interfaces
{
    public interface ILevel
    {
        public int Time { get; }
        public void RemoveTime();
    }
}