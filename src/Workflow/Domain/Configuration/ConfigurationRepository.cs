using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;

namespace Workflow.Domain.Configuration
{
    public interface IConfigurationRepository
    {
        Result Save(Draft draft);
        Maybe<Draft> GetDraft(ConfigurationId id);
        Result DraftExists(ConfigurationId id);
        Result Save(Planned planned);
        Maybe<Planned> GetPlanned(ConfigurationId id);
        Result PlannedExists(ConfigurationId id);
        Result Save(Live live);
        Maybe<Live> GetLive(ConfigurationId id);
        Result Save(Archive archived);
        Maybe<Archive> GetArchived(ConfigurationId id);
    }
}