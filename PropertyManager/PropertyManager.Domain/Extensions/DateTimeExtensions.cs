using System;

namespace PropertyManager.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStandardDateString(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string ToStandardDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy HH:mm");
        }
    }
}
