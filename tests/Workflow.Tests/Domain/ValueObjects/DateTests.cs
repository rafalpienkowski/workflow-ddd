using System;
using FluentAssertions;
using Xunit;

namespace Workflow.Domain.Configuration.ValueObjects
{
    public class DateTests
    {
        [Fact]
        public void Date_Should_Not_Contain_Time()
        {
            var now = DateTime.Now;

            var date = Date.FromDateTime(now);

            var dateTimeDate = date.AsDateTime();
            dateTimeDate.Year.Should().Be(now.Year);
            dateTimeDate.Month.Should().Be(now.Month);
            dateTimeDate.Day.Should().Be(now.Day);
            dateTimeDate.Hour.Should().Be(0);
            dateTimeDate.Minute.Should().Be(0);
            dateTimeDate.Second.Should().Be(0);
            dateTimeDate.Millisecond.Should().Be(0);
        }
    }
}