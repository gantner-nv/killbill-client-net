using System;

namespace KillBill.Client.Net.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateStringISO(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
        }

        public static string ToDateString(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd");
        }
    }
}