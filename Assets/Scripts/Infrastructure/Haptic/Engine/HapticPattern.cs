namespace CodeBase.Infrastructure.Haptic.Engine
{
    public static class HapticPattern
    {
        public static readonly long LightDuration;
        public static readonly long MediumDuration;
        public static readonly long HeavyDuration;
        public static readonly int LightAmplitude;
        public static readonly int MediumAmplitude;
        public static readonly int HeavyAmplitude;
        public static readonly long[] SuccessPattern;
        public static readonly int[] SuccessPatternAmplitude;
        public static readonly long[] WarningPattern;
        public static readonly int[] WarningPatternAmplitude;
        public static readonly long[] FailurePattern;
        public static readonly int[] FailurePatternAmplitude;

        static HapticPattern()
        {
            LightDuration = 20;
            MediumDuration = 40;
            HeavyDuration = 80;
            LightAmplitude = 40;
            MediumAmplitude = 120;
            HeavyAmplitude = 255;
            
            SuccessPattern = new[] { 0, LightDuration, LightDuration, HeavyDuration };
            SuccessPatternAmplitude = new[] { 0, LightAmplitude, 0, HeavyAmplitude };
            WarningPattern = new[] { 0, HeavyDuration, LightDuration, MediumDuration };
            WarningPatternAmplitude = new[] { 0, HeavyAmplitude, 0, MediumAmplitude };
            FailurePattern = new[] { 0, MediumDuration, LightDuration, MediumDuration, LightDuration, HeavyDuration, LightDuration, LightDuration };
            FailurePatternAmplitude = new[] { 0, MediumAmplitude, 0, MediumAmplitude, 0, HeavyAmplitude, 0, LightAmplitude };
        }
    }
}