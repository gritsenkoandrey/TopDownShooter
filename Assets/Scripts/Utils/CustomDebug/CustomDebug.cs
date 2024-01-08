using UnityEngine;

namespace CodeBase.Utils.CustomDebug
{
    public static class CustomDebug
    {
        public static void Log(string message, DebugColorType color = DebugColorType.Silver) => 
            Debug.Log($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
        
        public static void LogWarning(string message, DebugColorType color = DebugColorType.Yellow) => 
            Debug.LogWarning($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");

        public static void LogError(string message, DebugColorType color = DebugColorType.Red) => 
            Debug.LogError($"<color=#{DebugColorDictionary.Colors[color]}>{message}</color>");
    }
}