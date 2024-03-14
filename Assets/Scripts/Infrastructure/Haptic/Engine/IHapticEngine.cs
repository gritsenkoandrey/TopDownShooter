namespace CodeBase.Infrastructure.Haptic.Engine
{
    public interface IHapticEngine
    {
        void Play(HapticType type);
        void Mute();
        void Unmute();
    }
}