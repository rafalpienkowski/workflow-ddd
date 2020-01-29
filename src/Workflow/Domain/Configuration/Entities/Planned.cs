using System;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Domain.Configuration.Entities
{

    public class Planned
    {
        /// <summary>
        /// Id
        /// </summary>
        internal PlannedId Id { get; }
        
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

        /// <summary>
        /// When data go live
        /// </summary>
        internal Date WhenGoLive { get; }

        internal Planned(PlannedId id, Data data, Author author, Date goLive)
        {
            Id = id;
            Data = data;
            Author = author;
            WhenGoLive = goLive;
            CreationDate = Date.Now();
        }

        internal Planned(PlannedId id, Data data, Author author, Date creationDate, Date whenGoLive)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
            WhenGoLive = whenGoLive;
        }

        internal Live GoLive(Author author)
        {
            var liveId = LiveId.FromPlannedId(Id);
            return new Live(liveId, Data, author);
        }
    }
}