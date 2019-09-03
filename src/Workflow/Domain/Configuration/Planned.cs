using System;

namespace Workflow.Domain.Configuration
{
    public class Planned
    {
        /// <summary>
        /// Id
        /// </summary>
        internal int Id { get; }
        
        /// <summary>
        /// Data
        /// </summary>
        internal string Data { get; }
        /// <summary>
        /// Who mark as planned
        /// </summary>
        internal string Author { get; }

        /// <summary>
        /// When config was planned
        /// </summary>
        internal DateTime CreationDate { get; }

        /// <summary>
        /// When data go live
        /// </summary>
        internal DateTime WhenGoLive { get; }

        internal Planned(int id, string data, string author, DateTime goLive)
        {
            Id = id;
            Data = data;
            Author = author;
            WhenGoLive = goLive;
            CreationDate = DateTime.UtcNow;
        }

        internal Planned(int id, string data, string author, DateTime creationDate, DateTime whenGoLive)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
            WhenGoLive = whenGoLive;
        }

        internal Live GoLive(string author)
        {
            return new Live(Id, Data, author);
        }
    }
}