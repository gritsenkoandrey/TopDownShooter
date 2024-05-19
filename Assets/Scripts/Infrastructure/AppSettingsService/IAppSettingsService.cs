namespace CodeBase.Infrastructure.AppSettingsService
{
    public interface IAppSettingsService
    {
        void Init();
        void SetFrameRate(int rate);
    }
}