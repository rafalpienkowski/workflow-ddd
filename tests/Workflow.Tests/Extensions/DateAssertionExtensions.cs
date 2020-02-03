using System;
using FluentAssertions;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Tests.Extensions
{
    public static class DateExtensions
    {
        public static void ShouldBe(this Date date, DateTime dateTime)
        {
            var dateAsDateTime = date.AsDateTime();
            dateAsDateTime.Year.Should().Be(dateTime.Year);
            dateAsDateTime.Month.Should().Be(dateTime.Month);
            dateAsDateTime.Day.Should().Be(dateTime.Day);
        }
    }
}