using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;

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
            var idValue = ConfigurationId.FromGuid(id);
            var creationDate = Date.FromDateTime(creationDateTime);
            var whenGoLiveDate = Date.FromDateTime(whenGoLiveDateTime);

            var dataResult = Data.FromString(data);
            var authorResult = Author.FromString(author);
            
            Result.Combine(dataResult, authorResult)
                .OnFailure((r) => throw new ArgumentOutOfRangeException(r.Message));

            return new Planned(idValue, dataResult.Value, authorResult.Value, creationDate, whenGoLiveDate);
        }
    }
}