using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.ValueObjects;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.Factories
{
    /// <summary>
    /// Live factory is dedicated only for repository
    /// </summary>
    internal class LiveFactory
    {
        internal static Live Create(Guid id, string data, string author, DateTime creationDateTime)
        {
            var idValue = LiveId.FromGuid(id);
            var dataValue = Data.FromString(data);
            var authorValue = Author.FromString(author);
            var creationDate = Date.FromDateTime(creationDateTime);

            return new Live(idValue, dataValue, authorValue, creationDate);
        }
    }
}