using System;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
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

        public int Schedule(int id, string author, DateTime whenGoLive)
        {
            var draftId = DraftId.FromGuid(Guid.NewGuid());
            var draft = _repository.GetDraft(draftId);
            var planned = draft.Schedule(author, whenGoLive);
            return _repository.Save(planned);
        }

        public int GoLive(int id, string author)
        {
            Live live = null;
            var draftId = DraftId.FromGuid(Guid.NewGuid());
            if (_repository.DraftExists(id))
            {
                var draft = _repository.GetDraft(draftId);
                live = draft.GoLive(author);
            }
            
            if (_repository.PlannedExists(id))
            {
                var planned = _repository.GetPlanned(id);
                live = planned.GoLive(author);
            }
            else
            {
                throw new NotSupportedException($"There is no draft or planned configuration with id: {id}");
            }

            return _repository.Save(live);
        }

        public int Archive(int id, string author)
        {
            var live = _repository.GetLive(id);
            var archived = live.Archive(author);
            return _repository.Save(archived);
        }
    }
}