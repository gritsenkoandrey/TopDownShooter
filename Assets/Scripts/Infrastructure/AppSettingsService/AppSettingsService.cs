using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.Infrastructure.AppSettingsService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class AppSettingsService : IAppSettingsService
    {
        void IAppSettingsService.Init()
        {
            UnityEngine.Input.multiTouchEnabled = false;
            Debug.unityLogger.logEnabled = Debug.isDebugBuild;
        }

        void IAppSettingsService.SetFrameRate(int rate)
        {
            Application.targetFrameRate = rate;
        }
    }
}