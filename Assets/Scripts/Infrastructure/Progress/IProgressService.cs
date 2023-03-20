using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}