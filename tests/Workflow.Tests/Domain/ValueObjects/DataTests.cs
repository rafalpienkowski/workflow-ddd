using Workflow.Domain.Configuration.ValueObjects;
using Xunit;
using FluentAssertions;
using System;
using Workflow.Domain.Framework;
using System.Linq;

namespace Workflow.Tests.Domain.ValueObjects
{
    public class DataTests
    {

        [Fact]
        public void Short_Data_Are_Allowed()
        {
            var shortData = "Valid data";

            var data = Data.FromString(shortData);

            data.Should().NotBeNull();
        }

        [Fact]
        public void Long_Data_Are_Forbidden()
        {
            var rand = new Random();
            var longData = string.Join("", Enumerable.Repeat(0, 400).Select(n => (char)rand.Next(127)));

            Action act = () => Data.FromString(longData);

            act.Should()
                .Throw<BusinessException>()
                .WithMessage("Only short data are supported");
        }
    }
}