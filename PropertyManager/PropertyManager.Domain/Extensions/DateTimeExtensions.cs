using System;

namespace PropertyManager.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStandardDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string ToStandardDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
