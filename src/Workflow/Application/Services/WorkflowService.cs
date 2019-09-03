using System;
using Workflow.Domain.Configuration;

namespace Workflow.Application.Services
{
    public class WorkflowService
    {
        private readonly IConfigurationRepository _repository;

        public WorkflowService(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public int CreateDraft(string data, string author)
        {
            var factory = new DraftFactory();
            var draft = factory.Create(data, author);
            return _repository.Save(draft);
        }

        public int Schedule(int id, string author, DateTime whenGoLive)
        {
            var draft = _repository.GetDraft(id);
            var planned = draft.Schedule(author, whenGoLive);
            return _repository.Save(planned);
        }

        public int GoLive(int id, string author)
        {
            Live live = null;
            if (_repository.DraftExists(id))
            {
                var draft = _repository.GetDraft(id);
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