using System;

namespace Workflow.Domain.Configuration.ValueObjects
{
    public class PlannedId
    {
        private readonly Guid _id;

        private PlannedId(Guid id)
        {
            _id = id;
        }

        internal static PlannedId FromGuid(Guid id)
        {
            return new PlannedId(id);
        }

        public static PlannedId FromDraftId(DraftId draftId)
        {
            return new PlannedId(draftId.AsGuid());
        }

        internal Guid AsGuid() => _id;
    }
}