using System;

namespace Foundation.Core
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Days(this int @this) => TimeSpan.FromDays(@this);
        public static TimeSpan Seconds(this int @this) => TimeSpan.FromSeconds(@this);
        public static TimeSpan Minutes(this int @this) => TimeSpan.FromMinutes(@this);
        public static TimeSpan Hours(this int @this) => TimeSpan.FromHours(@this);
    }
}