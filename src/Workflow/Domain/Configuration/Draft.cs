using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration
{
    public class Draft
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
        /// Who create draft
        /// </summary>
        internal string Author { get; }

        /// <summary>
        /// When draft was created
        /// </summary>
        internal DateTime CreationDate { get; }

        internal Draft(string data, string author)
        {
            Data = data;
            Author = author;
            CreationDate = DateTime.UtcNow;
        }

        internal Draft(int id, string data, string author, DateTime creationDate)
        {
            Id = id;
            Data = data;
            Author = author;
            CreationDate = creationDate;
        }

        public Planned Schedule(string author, DateTime whenGoLive)
        {
            //When go live validation
            return new Planned(Id, Data, author, whenGoLive);
        }

        public Live GoLive(string author)
        {
            //Validation
            return new Live(Id, Data, author);
        }
    }
}