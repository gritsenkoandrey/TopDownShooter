using CodeBase.Infrastructure.Haptic.Engine;

namespace CodeBase.Infrastructure.Haptic
{
    public interface IHapticService
    {
        void Init();
        void Play(HapticType type);
        void IsEnable(bool isEnable);
    }
}