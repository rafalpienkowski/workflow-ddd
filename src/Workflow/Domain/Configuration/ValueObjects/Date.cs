using System;

namespace Workflow.Domain.Configuration.ValueObjects
{
    /// <summary>
    /// Date representation in our system.
    /// We don't care about time part.
    /// </summary>
    public class Date
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;

        private Date(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
        }

        public static Date FromDateTime(DateTime dateTime)
        {
            return new Date(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static Date Now()
        {
            var now = DateTime.UtcNow;
            return FromDateTime(now);
        }

        public DateTime AsDateTime() => new DateTime(_year, _month, _day);
    }
}