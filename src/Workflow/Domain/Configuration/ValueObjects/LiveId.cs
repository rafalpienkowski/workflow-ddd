using System;

namespace Workflow.Domain.Configuration.ValueObjects
{
    public class LiveId
    {
        private readonly Guid _id;

        private LiveId(Guid id)
        {
            _id = id;
        }

        public static LiveId FromGuid(Guid id) => new LiveId(id);

        public static LiveId FromDraftId(DraftId draftId) => new LiveId(draftId.AsGuid());

        public static LiveId FromPlannedId(PlannedId plannedId) => new LiveId(plannedId.AsGuid());

        internal Guid AsGuid() => _id;
    }
}