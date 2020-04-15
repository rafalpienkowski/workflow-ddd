using Workflow.Domain.Configuration.ValueObjects;
using Xunit;
using FluentAssertions;

namespace Workflow.Tests.Domain.ValueObjects
{
    public class AuthorTests
    {
        
        [Fact]
        public void Only_Short_Letter_Authors_Allowed()
        {
            var shortName = "Tim";
            var authorResult = Author.FromString(shortName);

            authorResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Author_With_Longer_Names_Are_Not_Allowed()
        {
            var longName = "This is some long name";

            var authorResult = Author.FromString(longName);

            authorResult.IsFailure.Should().BeTrue();
            authorResult.Message.Should().Be("Author's name is too long");
        }

        [Fact]
        public void Only_T_Authors_Are_Allowed()
        {
            var notAllowedName = "Bob";

            var authorResult = Author.FromString(notAllowedName);

            authorResult.IsFailure.Should().BeTrue();
            authorResult.Message.Should().Be("Only authors with names starting with T are allowed");
        }
    }
}