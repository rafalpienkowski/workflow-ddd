using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.Factories
{
    /// <summary>
    /// Archive factory is dedicated only for repository
    /// </summary>
    internal class ArchiveFactory
    {
        internal static Archive Create(Guid id, string data, string author, DateTime creationDateTime)
        {
            var idValue = ConfigurationId.FromGuid(id);
            var dataResult = Data.FromString(data);
            var authorResult = Author.FromString(author);
            var creationDate = Date.FromDateTime(creationDateTime);

            return new Archive(idValue, dataResult.Value, authorResult.Value, creationDate);
        }
    }
}