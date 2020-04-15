using Workflow.Domain.Configuration.ValueObjects;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;

namespace Workflow.Tests.Domain.ValueObjects
{
    public class DataTests
    {

        [Fact]
        public void Short_Data_Are_Allowed()
        {
            var shortData = "Valid data";

            var dataResult = Data.FromString(shortData);

            dataResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Long_Data_Are_Forbidden()
        {
            var rand = new Random();
            var longData = string.Join("", Enumerable.Repeat(0, 400).Select(n => (char)rand.Next(127)));

            var dataResult = Data.FromString(longData);

            dataResult.IsFailure.Should().BeTrue();
            dataResult.Message.Should().Be("Only short data are supported");
        }
    }
}