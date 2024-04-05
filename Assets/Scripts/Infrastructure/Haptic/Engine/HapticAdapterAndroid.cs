using UnityEngine;

namespace CodeBase.Infrastructure.Haptic.Engine
{
    public sealed class HapticAdapterAndroid : IHapticAdapter
    {
        private const int MinSDKVersion = 26;

        private readonly AndroidJavaObject _androidVibrator;
        private AndroidJavaClass _vibrationEffectClass;

        private int _sdkVersion = -1;

        public HapticAdapterAndroid()
        {
            _androidVibrator = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                .GetStatic<AndroidJavaObject>("currentActivity")
                .Call<AndroidJavaObject>("getSystemService", "vibrator");
        }

        void IHapticAdapter.Play(HapticType type) => Haptic(type);

        bool IHapticAdapter.IsSupported() => 
            _androidVibrator != null && _androidVibrator.Call<bool>("hasVibrator");

        private void Haptic(HapticType type)
        {
            try
            {
                switch (type)
                {
                    case HapticType.Selection:
                        AndroidVibrate(HapticPattern.LightDuration, HapticPattern.LightAmplitude);
                        break;
                    case HapticType.Success:
                        AndroidVibrate(HapticPattern.SuccessPattern, HapticPattern.SuccessPatternAmplitude, -1);
                        break;
                    case HapticType.Warning:
                        AndroidVibrate(HapticPattern.WarningPattern, HapticPattern.WarningPatternAmplitude, -1);
                        break;
                    case HapticType.Failure:
                        AndroidVibrate(HapticPattern.FailurePattern, HapticPattern.FailurePatternAmplitude, -1);
                        break;
                    case HapticType.Light:
                        AndroidVibrate(HapticPattern.LightDuration, HapticPattern.LightAmplitude);
                        break;
                    case HapticType.Medium:
                        AndroidVibrate(HapticPattern.MediumDuration, HapticPattern.MediumAmplitude);
                        break;
                    case HapticType.Heavy:
                        AndroidVibrate(HapticPattern.HeavyDuration, HapticPattern.HeavyAmplitude);
                        break;
                    case HapticType.Vibrate:
                        Handheld.Vibrate();
                        break;
                }
            }
            catch (System.NullReferenceException exception)
            {
                Utils.CustomDebug.CustomDebug.LogError(exception.StackTrace);
            }
        }

        private void AndroidVibrate(long milliseconds)
        {
            _androidVibrator?.Call("vibrate", milliseconds);
        }

        private void AndroidVibrate(long[] pattern, int[] amplitudes, int repeat)
        {
            if (AndroidSDKVersion() < MinSDKVersion)
            {
                _androidVibrator?.Call("vibrate", pattern, repeat);
            }
            else
            {
                VibrationEffectClassInitialization();
                
                if (_vibrationEffectClass != null)
                {
                    CreateVibrationEffect("createWaveform", new object[] { pattern, amplitudes, repeat });
                }
            }
        }

        private void AndroidVibrate(long milliseconds, int amplitude)
        {
            if (AndroidSDKVersion() < MinSDKVersion)
            {
                AndroidVibrate(milliseconds);
            }
            else
            {
                VibrationEffectClassInitialization();
                
                if (_vibrationEffectClass != null)
                {
                    if (CreateVibrationEffect("createOneShot", new object[] { milliseconds, amplitude }) == false)
                    {
                        AndroidVibrate(milliseconds);
                    }
                }
                else
                {
                    AndroidVibrate(milliseconds);
                }
            }
        }

        private bool CreateVibrationEffect(string function, params object[] args)
        {
            if (_androidVibrator != null)
            {
                AndroidJavaObject vibrationEffect = _vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
                
                if (vibrationEffect != null)
                {
                    _androidVibrator.Call("vibrate", vibrationEffect);
                    
                    return true;
                }
            }

            return false;
        }

        private void VibrationEffectClassInitialization()
        {
            _vibrationEffectClass ??= new AndroidJavaClass("android.os.VibrationEffect");
        }

        private int AndroidSDKVersion()
        {
            if (_sdkVersion == -1)
            {
                using AndroidJavaClass version = new AndroidJavaClass("android/os/Build$VERSION");
                _sdkVersion = version.GetStatic<int>("SDK_INT");
            }

            return _sdkVersion;
        }
    }
}