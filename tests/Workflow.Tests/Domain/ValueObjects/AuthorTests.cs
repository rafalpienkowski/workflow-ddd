using Workflow.Domain.Configuration.ValueObjects;
using Xunit;
using FluentAssertions;
using System;
using Workflow.Domain.Framework;

namespace Workflow.Tests.Domain.ValueObjects
{
    public class AuthorTests
    {
        
        [Fact]
        public void Only_Short_Letter_Authors_Allowed()
        {
            var shortName = "Tim";
            var author = Author.FromString(shortName);

            author.Should().NotBeNull();
        }

        [Fact]
        public void Author_With_Longer_Names_Are_Not_Allowed()
        {
            var longName = "This is some long name";

            Action act = () => Author.FromString(longName);

            act.Should()
                .Throw<BusinessException>()
                .WithMessage("Author's name is too long");
        }

        [Fact]
        public void Only_T_Authors_Are_Allowed()
        {
            var notAllowedName = "Bob";

            Action act = () => Author.FromString(notAllowedName);

            act.Should()
                .Throw<BusinessException>()
                .WithMessage("Only authors with names starting with T are allowed");
        }
    }
}