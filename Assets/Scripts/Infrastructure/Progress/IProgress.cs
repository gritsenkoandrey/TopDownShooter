namespace CodeBase.Infrastructure.Progress
{
    public interface IProgress { }

    public interface IProgressReader : IProgress
    {
        public void Read(PlayerProgress progress);
    }
    
    public interface IProgressWriter : IProgress
    {
        public void Write(PlayerProgress progress);
    }
}