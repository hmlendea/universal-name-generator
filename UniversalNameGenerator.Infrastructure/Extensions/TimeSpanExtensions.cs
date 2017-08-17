using System;

namespace UniversalNameGenerator.Infrastructure.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Multiply(this TimeSpan source, int multiplier)
        {
            return TimeSpan.FromTicks(source.Ticks * multiplier);
        }

        public static TimeSpan Multiply(this TimeSpan source, double multiplier)
        {
            return TimeSpan.FromTicks((long)(source.Ticks * multiplier));
        }

        public static TimeSpan Multiply(this TimeSpan source, float multiplier)
        {
            return TimeSpan.FromTicks((long)(source.Ticks * multiplier));
        }
    }
}
