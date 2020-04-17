using System;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.Factories;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;

namespace Workflow.Application.Services
{
    public class WorkflowService
    {
        private readonly IConfigurationRepository _repository;

        public WorkflowService(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public Result<Draft> CreateDraft(string data, string author)
        {
            return DraftFactory.Create(data, author)
                .OnSuccess(result =>_repository.Save(result.Value))
                .OnBoth(result => result);
        }

        public Result Schedule(Guid id, string author, DateTime whenGoLive)
        {
            var authorResult = Author.FromString(author);
            var configurationId = ConfigurationId.FromGuid(id);
            var whenGoLiveValue = Date.FromDateTime(whenGoLive);

            var draft = _repository.GetDraft(configurationId);

            return Result.Combine(authorResult, (Result<Draft>)draft)
                .OnSuccess(() => draft.Value.Schedule(authorResult.Value, whenGoLiveValue)
                                        .OnSuccess(result => _repository.Save(result.Value))
                            );
        }

        public Result GoLive(Guid id, string author)
        {
            Live live = null;
            var configurationId = ConfigurationId.FromGuid(id);
            var authorResult = Author.FromString(author);

            if (authorResult.IsFailure)
            {
                return Result.Failure(authorResult.Message);
            }

            if (_repository.DraftExists(configurationId).IsSuccess)
            {
                var draftResult = _repository.GetDraft(configurationId);
                live = draftResult.Value.GoLive(authorResult.Value);
            }
            
            if (_repository.PlannedExists(configurationId).IsSuccess)
            {
                var plannedResult = _repository.GetPlanned(configurationId);
                live = plannedResult.Value.GoLive(authorResult.Value);
            }
            else
            {
                return Result.Failure("There is no draft or planned configuration with id: {id}");
            }

            return _repository.Save(live);
        }

        public Result Archive(Guid id, string author)
        {
            var configurationId = ConfigurationId.FromGuid(id);
            var authorResult = Author.FromString(author);
            var liveResult = _repository.GetLive(configurationId);
            
            return Result.Combine(authorResult, (Result<Live>)liveResult)
                .OnSuccess(() => 
                    liveResult.Value.Archive(authorResult.Value)
                        .OnSuccess(result => _repository.Save(result.Value))
                );
        }
    }
}