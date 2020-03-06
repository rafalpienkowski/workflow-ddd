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

        public static Author FromString(string author)
        {
            if (author.Length > 3)
            {
                throw new BusinessException("Author's name is too long");
            }
            if (!author.StartsWith("T"))
            {
                throw new BusinessException("Only authors with names starting with T are allowed");
            }
            return new Author(author);
        } 

        public string AsString() => _author;
    }
}