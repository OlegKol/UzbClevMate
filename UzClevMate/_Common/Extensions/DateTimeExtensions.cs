using System;
using System.Globalization;

namespace UzClevMate._Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToISO8601DateFormat(this DateTime date)
        {
            return date.ToString(_Definitions.ISO8601DateFormat, CultureInfo.InvariantCulture);
        }

        public static string ToISO8601FullDateFormat(this DateTime date)
        {
            return date.ToString(_Definitions.ISO8601FullDateFormat, CultureInfo.InvariantCulture);
        }
    }
}