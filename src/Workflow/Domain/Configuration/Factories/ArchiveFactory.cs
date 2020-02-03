using System;
using System.Runtime.CompilerServices;
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
            var idValue = ArchiveId.FromGuid(id);
            var dataValue = Data.FromString(data);
            var authorValue = Author.FromString(author);
            var creationDate = Date.FromDateTime(creationDateTime);

            return new Archive(idValue, dataValue, authorValue, creationDate);
        }
    }
}