using Workflow.Domain.Framework;

namespace Workflow.Domain.Configuration.ValueObjects
{
    /// <summary>
    /// This class represents an author
    /// </summary>
    public class Author
    {
        private readonly string _author;

        private Author(string author)
        {
            _author = author;
        }

        public static Result<Author> FromString(string author)
        {
            if (author.Length > 3)
            {
                return Result<Author>.Failure("Author's name is too long");
            }
            if (!author.StartsWith("T"))
            {
                return Result<Author>.Failure("Only authors with names starting with T are allowed");
            }
            return Result<Author>.Success(new Author(author));
        } 

        public string AsString() => _author;
    }
}