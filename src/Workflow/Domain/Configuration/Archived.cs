using System;

namespace Workflow.Domain.Configuration
{
    public class Archived
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

        internal Archived(int id,string data, string author)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = DateTime.UtcNow;
        }

        internal Archived(int id, string data, string author, DateTime creationDate)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
        }
    }
}