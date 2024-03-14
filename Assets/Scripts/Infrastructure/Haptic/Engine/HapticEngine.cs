namespace CodeBase.Infrastructure.Haptic.Engine
{
    public sealed class HapticEngine : IHapticEngine
    {
        private readonly IHapticAdapter _adapter;
        private bool _isMuted;

        public HapticEngine()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _adapter = new HapticAdapterAndroid();
#else
            _adapter = new HapticAdapterDummy();
#endif
        }

        void IHapticEngine.Play(HapticType type)
        {
            if (_isMuted && _adapter.IsSupported() == false) return;
            
            _adapter.Play(type);
        }

        void IHapticEngine.Mute() => _isMuted = true;

        void IHapticEngine.Unmute() => _isMuted = false;
    }
}