using System;

namespace WebApi.Extensions
{
    public static class DateTimeExtension
    {
        public static long ToUnixTime(this DateTime date)
        {
            var timeSpan = (date - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}