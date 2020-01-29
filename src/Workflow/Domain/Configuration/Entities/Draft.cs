using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.ValueObjects;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.Entities
{
    public class Draft
    {
        /// <summary>
        /// Id
        /// </summary>
        internal DraftId Id { get; }
        
        /// <summary>
        /// Data
        /// </summary>
        internal Data Data { get; }

        /// <summary>
        /// Who create draft
        /// </summary>
        internal Author Author { get; }

        /// <summary>
        /// When draft was created
        /// </summary>
        internal Date CreationDate { get; }

        internal Draft(Data data, Author author)
        {
            Data = data;
            Author = author;
            CreationDate = Date.FromDateTime(DateTime.UtcNow);
        }

        internal Draft(DraftId id, Data data, Author author, Date creationDate)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
        }

        public Planned Schedule(Author author, Date whenGoLive)
        {
            //When go live validation
            var plannedId = PlannedId.FromDraftId(Id);
            return new Planned(plannedId, Data, author, whenGoLive);
        }

        public Live GoLive(Author author)
        {
            var liveId = LiveId.FromDraftId(Id);
            return new Live(liveId, Data, author);
        }
    }
}