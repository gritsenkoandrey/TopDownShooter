namespace CodeBase.Infrastructure.Progress
{
    public sealed class ProgressService : IProgressService
    {
        PlayerProgress IProgressService.PlayerProgress { get; set; }
    }
}