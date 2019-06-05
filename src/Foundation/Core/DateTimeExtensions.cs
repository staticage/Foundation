using System;
using System.Collections.Generic;

namespace Foundation.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime GetStartDate(this DateTime dateTime)
        {
            return dateTime.ToLocalTime().Date;
        }

        public static DateTime GetEndDate(this DateTime dateTime)
        {
            return dateTime.ToLocalTime().Date.AddDays(1).AddMilliseconds(-1);
        }

        public static DateTime GetStartMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        public static DateTime GetEndMonth(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }

        public static DateTime GetStartMonthDate(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).GetStartDate();
        }

        public static DateTime GetEndMonthDate(this DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).GetEndDate();
        }
        
        public static DateTime GetMondayDate(this DateTime datetime)
        {
            int i = datetime.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return datetime.Subtract(ts);
        }
        public static DateTime GetMondayStartDate(this DateTime datetime)
        {
            return datetime.GetMondayDate().GetStartDate();
        }

        public static DateTime GetSundayDate(this DateTime datetime)
        {
            int i = datetime.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return datetime.Add(ts);
        }

        public static DateTime GetSundayEndDate(this DateTime datetime)
        {
            return datetime.GetSundayDate().GetEndDate();
        }


        public static DateTime GetUTCStartDate(this DateTime dateTime)
        {
            return dateTime.GetStartDate().ToUniversalTime();
        }

        public static DateTime GetUTCEndDate(this DateTime dateTime)
        {
            return dateTime.GetEndDate().ToUniversalTime();
        }

        public static List<DateTime> GetDates(this DateTime startTime, DateTime endTime)
        {
            List<DateTime> dates = new List<DateTime>();
            for (var i = startTime; i <= endTime; i = i.AddDays(1))
            {
                dates.Add(i);
            }
            return dates;
        }

        public static ushort GetAgeByBirthday(this DateTime? birth)
        {
            if (birth == null)
            {
                return 0;
            }
            DateTime birthDate = (DateTime)birth;
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return (ushort)age;
        }

        public static int GetWeek(this DateTime dateTime) =>
            new System.Globalization.GregorianCalendar().GetWeekOfYear(dateTime, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        public static string ToDateString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");
        public static string ToTimeString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm");
        public static string ToShortTimeString(this DateTime dateTime) => $"{dateTime.Month}-{dateTime.Day}";
        public static DateTime GetDate(this string dateTime) => Convert.ToDateTime(dateTime);
    }
}
