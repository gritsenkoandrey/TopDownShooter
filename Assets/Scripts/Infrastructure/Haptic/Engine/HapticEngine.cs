namespace CodeBase.Infrastructure.Haptic.Engine
{
    public sealed class HapticEngine : IHapticEngine
    {
        private readonly IHapticAdapter _adapter;
        private bool _isEnable;

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
            if (_isEnable == false && _adapter.IsSupported() == false) return;
            
            _adapter.Play(type);
        }

        public void IsEnable(bool isEnable) => _isEnable = isEnable;
    }
}