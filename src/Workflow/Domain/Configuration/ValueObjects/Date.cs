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

        private Date(DateTime date)
        {
            _year = date.Year;
            _month = date.Month;
            _day = date.Day;
        }

        public static Date FromDateTime(DateTime dateTime) => new Date(dateTime);

        public DateTime AsDateTime() => new DateTime(_year, _month, _day);
    }
}