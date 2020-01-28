using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Domain.Configuration
{
    public interface IConfigurationRepository
    {
        void Save(Draft draft);
        Draft GetDraft(DraftId id);
        bool DraftExists(int id);
        int Save(Planned planned);
        Planned GetPlanned(int id);
        bool PlannedExists(int id);
        int Save(Live live);
        Live GetLive(int id);
        int Save(Archived archived);
        Archived GetArchived(int id);
    }
}