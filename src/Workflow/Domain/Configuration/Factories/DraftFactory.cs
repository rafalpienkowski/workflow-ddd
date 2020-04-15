using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.Factories
{
    /// <summary>
    /// Starting point. 
    /// The first step in our workflow is to create a draft of a document. 
    /// </summary>
    public class DraftFactory
    {
        /// <summary>
        /// This is the right way our system should creates draft instances.
        /// </summary>
        public static Draft Create(string data, string author)
        {
            var configurationId = ConfigurationId.New();
            var draftData = Data.FromString(data);
            var draftAuthor = Author.FromString(author);
            return new Draft(configurationId, draftData, draftAuthor);
        }

        /// <summary>
        /// Creates a draft objects based on values stored in the DB.
        /// </summary>
        internal static Draft Create(Guid id, string data, string author, DateTime creationDate)
        {
            var configurationId = ConfigurationId.FromGuid(id);
            var draftData = Data.FromString(data);
            var draftAuthor = Author.FromString(author);
            var draftCreationDate = Date.FromDateTime(creationDate);

            return new Draft(configurationId, draftData, draftAuthor, draftCreationDate);
        }
    }
}