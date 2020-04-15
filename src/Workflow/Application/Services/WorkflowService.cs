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

        public void Schedule(Guid id, string author, DateTime whenGoLive)
        {
            var configurationId = ConfigurationId.FromGuid(id);
            var draft = _repository.GetDraft(configurationId);
            var authorResult = Author.FromString(author);
            var whenGoLiveValue = Date.FromDateTime(whenGoLive);
            var planned = draft.Schedule(authorResult.Value, whenGoLiveValue);
            
            _repository.Save(planned);
        }

        public void GoLive(Guid id, string author)
        {
            Live live = null;
            var configurationId = ConfigurationId.FromGuid(id);
            var authorResult = Author.FromString(author);
            if (_repository.DraftExists(configurationId))
            {
                var draft = _repository.GetDraft(configurationId);
                live = draft.GoLive(authorResult.Value);
            }
            
            if (_repository.PlannedExists(configurationId))
            {
                var planned = _repository.GetPlanned(configurationId);
                live = planned.GoLive(authorResult.Value);
            }
            else
            {
                throw new NotSupportedException($"There is no draft or planned configuration with id: {id}");
            }

            _repository.Save(live);
        }

        public void Archive(Guid id, string author)
        {
            var authorResult = Author.FromString(author);
            var configurationId = ConfigurationId.FromGuid(id);
            var live = _repository.GetLive(configurationId);
            var archived = live.Archive(authorResult.Value);

            _repository.Save(archived);
        }
    }
}