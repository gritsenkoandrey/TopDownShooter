using System;
using System.Globalization;

namespace CodeBase.Utils
{
    public static class FormatTime
    {
        private const string ms = @"mm\:ss";
        private const string hms = @"hh\:mm\:ss";
        private const string dhms = @"dd\:hh\:mm\:ss";

        public static string SecondsToTime(int seconds)
        {
            int hours = seconds / 3600;

            if (hours > 24)
            {
                return Convert(dhms, seconds);
            }

            if (hours > 0)
            {
                return Convert(hms, seconds);
            }

            return Convert(ms, seconds);
        }
        
        private static string Convert(string format, int seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}