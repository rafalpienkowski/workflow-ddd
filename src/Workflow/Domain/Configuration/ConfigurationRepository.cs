using System;
using System.Collections.Generic;
using System.Linq;

namespace Workflow.Domain.Configuration
{
    public interface IConfigurationRepository
    {
        int Save(Draft draft);
        Draft GetDraft(int id);
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