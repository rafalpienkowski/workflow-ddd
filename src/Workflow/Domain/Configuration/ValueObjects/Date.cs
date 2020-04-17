using System;

namespace Workflow.Domain.Configuration.ValueObjects
{
    /// <summary>
    /// Date representation in our system.
    /// We don't care about time part.
    /// </summary>
    public class Date : IComparable<Date>
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;

        internal int Year => _year;
        internal int Month => _month;
        internal int Day => _day;

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

        public override bool Equals(object obj)
        {
            var other = obj as Date;
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            return this.CompareTo(other) == 0;
        }
 
        public override int GetHashCode() => _year * _month * _day * 3;
 
        public int CompareTo(Date other)
        {
            var thatDt = new DateTime(_year, _month, _day);
            var otherDt = new DateTime(other.Year, other.Month, other.Day);

            return thatDt.CompareTo(otherDt);
        }

        public static bool operator <(Date left, Date right) => left.CompareTo(right) == -1;

        public static bool operator >(Date left, Date right) => left.CompareTo(right) == 1;
    }
}