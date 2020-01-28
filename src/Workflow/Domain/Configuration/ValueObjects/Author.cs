
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

        public static Author FromString(string author) => new Author(author);

        public string AsString() => _author;
    }
}