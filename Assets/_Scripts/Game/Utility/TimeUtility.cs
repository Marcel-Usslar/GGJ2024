using System;
using System.Text;

namespace Game.Utility
{
    public static class TimeUtility
    {
        public static string GetFormattedTime(this double time)
        {
            return FormatTimer(TimeSpan.FromSeconds(time));
        }

        private static string FormatTimer(this TimeSpan time)
        {
            var builder = new StringBuilder();

            if (time.TotalMinutes >= 1)
                builder.Append($"{ShortTime(time.Minutes)}:");

            builder.Append($"{ShortTime(time.Seconds)}:{ShortTime(RoundMilliseconds(time.Milliseconds))}");

            return builder.ToString();
        }

        private static string ShortTime(int value)
        {
            return $"{value:D2}";
        }

        private static int RoundMilliseconds(int value)
        {
            return value / 10;
        }
    }
}