using System;
using System.Runtime.CompilerServices;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;

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
        public static Result<Draft> Create(string data, string author)
        {
            var configurationId = ConfigurationId.New();

            var draftDataResult = Data.FromString(data);
            var draftAuthorResult = Author.FromString(author);

            var combinedResult = Result.Combine(draftDataResult, draftAuthorResult);
            if (combinedResult.IsFailure)
            {
                return Result.Failure<Draft>(combinedResult.Message);
            }

            return Result.Success<Draft>(new Draft(configurationId, draftDataResult.Value, draftAuthorResult.Value));
        }

        /// <summary>
        /// Creates a draft objects based on values stored in the DB.
        /// </summary>
        internal static Result<Draft> Create(Guid id, string data, string author, DateTime creationDate)
        {
            var configurationId = ConfigurationId.FromGuid(id);
            var draftCreationDate = Date.FromDateTime(creationDate);

            var draftDataResult = Data.FromString(data);
            var draftAuthorResult = Author.FromString(author);

            Result.Combine(draftDataResult, draftAuthorResult)
                .OnFailure((r) => throw new ArgumentOutOfRangeException(r.Message));

            return Result.Success<Draft>(new Draft(configurationId, draftDataResult.Value, draftAuthorResult.Value, draftCreationDate));
        }
    }
}