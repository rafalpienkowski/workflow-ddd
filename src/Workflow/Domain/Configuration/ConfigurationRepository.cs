using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Domain.Configuration
{
    public interface IConfigurationRepository
    {
        void Save(Draft draft);
        Draft GetDraft(DraftId id);
        bool DraftExists(DraftId id);
        void Save(Planned planned);
        Planned GetPlanned(PlannedId id);
        bool PlannedExists(PlannedId id);
        void Save(Live live);
        Live GetLive(LiveId id);
        void Save(Archive archived);
        Archive GetArchived(ArchiveId id);
    }
}