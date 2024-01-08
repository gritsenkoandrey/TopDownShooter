using System.Collections.Generic;

namespace CodeBase.Utils.CustomDebug
{
    public static class DebugColorDictionary
    {
        private const string Gray    = "808080";
        private const string Silver  = "C0C0C0";
        private const string White   = "FFFFFF";
        private const string Fuchsia = "FF00FF";
        private const string Purple  = "800080";
        private const string Red     = "FF0000";
        private const string Maroon  = "800000";
        private const string Yellow  = "FFFF00";
        private const string Olive   = "808000";
        private const string Lime    = "00FF00";
        private const string Green   = "008000";
        private const string Aqua    = "00FFFF";
        private const string Teal    = "008080";
        private const string Blue    = "0000FF";
        private const string Navy    = "000080";

        private static readonly Dictionary<DebugColorType, string> _colors;
        
        static DebugColorDictionary()
        {
            _colors = new Dictionary<DebugColorType, string>
            {
                { DebugColorType.Gray, Gray },
                { DebugColorType.Silver, Silver },
                { DebugColorType.White, White },
                { DebugColorType.Fuchsia, Fuchsia },
                { DebugColorType.Purple, Purple },
                { DebugColorType.Red, Red },
                { DebugColorType.Maroon, Maroon },
                { DebugColorType.Yellow, Yellow },
                { DebugColorType.Olive, Olive },
                { DebugColorType.Lime, Lime },
                { DebugColorType.Green, Green },
                { DebugColorType.Aqua, Aqua },
                { DebugColorType.Teal, Teal },
                { DebugColorType.Blue, Blue },
                { DebugColorType.Navy, Navy },
            };
        }

        public static IReadOnlyDictionary<DebugColorType, string> Colors => _colors;
    }
}