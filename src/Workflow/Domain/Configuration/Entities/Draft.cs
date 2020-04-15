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
        internal ConfigurationId Id { get; }
        
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

        internal Draft(ConfigurationId id, Data data, Author author)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = Date.FromDateTime(DateTime.UtcNow);
        }

        internal Draft(ConfigurationId id, Data data, Author author, Date creationDate)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
        }

        public Planned Schedule(Author author, Date whenGoLive)
        {
            //When go live validation
            return new Planned(Id, Data, author, whenGoLive);
        }

        public Live GoLive(Author author)
        {
            return new Live(Id, Data, author);
        }
    }
}