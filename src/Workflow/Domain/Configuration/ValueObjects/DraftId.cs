using System;

namespace Workflow.Domain.Configuration.ValueObjects
{
    /// <summary>
    /// Represents draft identity
    /// </summary>
    public class DraftId
    {
        private readonly Guid _id;

        private DraftId(Guid id)
        {
            _id = id;
        }

        public int AsInt() => 0;

        public static DraftId FromGuid(Guid id) => new DraftId(id);

        public static DraftId New() => new DraftId(Guid.NewGuid());
    }
}