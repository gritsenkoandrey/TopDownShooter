namespace CodeBase.Infrastructure.Haptic.Engine
{
    public sealed class HapticAdapterDummy : IHapticAdapter
    {
        void IHapticAdapter.Play(HapticType type)
        {
#if UNITY_EDITOR
            Utils.CustomDebug.CustomDebug.Log($"Impact haptic {type}");
#endif
        }

        bool IHapticAdapter.IsSupported()
        {
#if UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }
}