using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;

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
            var idValue = ConfigurationId.FromGuid(id);
            var creationDate = Date.FromDateTime(creationDateTime);

            var dataResult = Data.FromString(data);
            var authorResult = Author.FromString(author);

            Result.Combine(dataResult, authorResult)
                .OnFailure((r) => throw new ArgumentOutOfRangeException(r.Message));

            return new Live(idValue, dataResult.Value, authorResult.Value, creationDate);
        }
    }
}