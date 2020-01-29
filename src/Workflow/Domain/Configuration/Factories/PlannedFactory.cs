using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.Factories
{

    /// <summary>
    /// Planned factory is dedicated only for repository
    /// </summary>
    internal class PlannedFactory
    {
        internal static Planned Create(Guid id, string data, string author, DateTime creationDateTime, DateTime whenGoLiveDateTime)
        {
            var idValue = PlannedId.FromGuid(id);
            var dataValue = Data.FromString(data);
            var authorValue = Author.FromString(author);
            var creationDate = Date.FromDateTime(creationDateTime);
            var whenGoLiveDate = Date.FromDateTime(whenGoLiveDateTime);

            return new Planned(idValue, dataValue, authorValue, creationDate, whenGoLiveDate);
        }
    }
}