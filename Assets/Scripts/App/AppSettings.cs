using UnityEngine;

namespace CodeBase.App
{
    public sealed class AppSettings
    {
        public void SetSettings()
        {
            Application.targetFrameRate = 60;

            if (!Debug.isDebugBuild)
            {
                Input.multiTouchEnabled = false;
                Debug.unityLogger.logEnabled = false;
            }
        }
    }
}