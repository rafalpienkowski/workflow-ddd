using System;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.Factories;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Application.Services
{
    public class WorkflowService
    {
        private readonly IConfigurationRepository _repository;

        public WorkflowService(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public Draft CreateDraft(string data, string author)
        {
            var draft = DraftFactory.Create(data, author);
            _repository.Save(draft);

            return draft;
        }

        public void Schedule(int id, string author, DateTime whenGoLive)
        {
            var draftId = DraftId.FromGuid(Guid.NewGuid());
            var draft = _repository.GetDraft(draftId);
            var authorValue = Author.FromString(author);
            var whenGoLiveValue = Date.FromDateTime(whenGoLive);
            var planned = draft.Schedule(authorValue, whenGoLiveValue);
            
            _repository.Save(planned);
        }

        public void GoLive(Guid id, string author)
        {
            Live live = null;
            var draftId = DraftId.FromGuid(id);
            var authorValue = Author.FromString(author);
            if (_repository.DraftExists(draftId))
            {
                var draft = _repository.GetDraft(draftId);
                live = draft.GoLive(authorValue);
            }
            
            var plannedId = PlannedId.FromDraftId(draftId);
            if (_repository.PlannedExists(plannedId))
            {
                var planned = _repository.GetPlanned(plannedId);
                live = planned.GoLive(authorValue);
            }
            else
            {
                throw new NotSupportedException($"There is no draft or planned configuration with id: {id}");
            }

            _repository.Save(live);
        }

        public void Archive(Guid id, string author)
        {
            var authorValue = Author.FromString(author);
            var liveId = LiveId.FromGuid(id);
            var live = _repository.GetLive(liveId);
            var archived = live.Archive(authorValue);

            _repository.Save(archived);
        }
    }
}