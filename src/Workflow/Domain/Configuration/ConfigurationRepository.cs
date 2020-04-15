using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Domain.Configuration
{
    public interface IConfigurationRepository
    {
        void Save(Draft draft);
        Draft GetDraft(ConfigurationId id);
        bool DraftExists(ConfigurationId id);
        void Save(Planned planned);
        Planned GetPlanned(ConfigurationId id);
        bool PlannedExists(ConfigurationId id);
        void Save(Live live);
        Live GetLive(ConfigurationId id);
        void Save(Archive archived);
        Archive GetArchived(ConfigurationId id);
    }
}