using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Domain.Configuration
{

    public class Live
    {
        /// <summary>
        /// Id
        /// </summary>
        internal LiveId Id { get; }
        
        /// <summary>
        /// Data
        /// </summary>
        internal Data Data { get; }

        /// <summary>
        /// Who mark as planned
        /// </summary>
        internal Author Author { get; }

        /// <summary>
        /// When config was planned
        /// </summary>
        internal Date CreationDate { get; }

        internal Live(LiveId id, Data data, Author author)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = Date.Now();
        }

        internal Live(LiveId id, Data data, Author author, Date creationDate)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
        }

        public Archive Archive(Author author)
        {
            var archivedId = ArchiveId.FromLiveId(Id);
            return new Archive(archivedId, Data, author);
        }
    }
}