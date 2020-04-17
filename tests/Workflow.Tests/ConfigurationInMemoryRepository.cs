using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.Factories;
using Workflow.Domain.Configuration.ValueObjects;
using Workflow.Domain.Framework;

namespace Workflow.Tests
{
    public class ConfigurationInMemoryRepository : IConfigurationRepository
    {
        private readonly List<SampleDataTable> _data = new List<SampleDataTable>();

        public Result Save(Draft draft)
        {
            var dataRow = new SampleDataTable
            {
                Id = draft.Id.AsGuid(),
                Status = ConfigStatus.Draft,
                Data = draft.Data.AsString(),
                DraftCreation = draft.CreationDate.AsDateTime(),
                DraftAuthor = draft.Author.AsString()
            };
            _data.Add(dataRow);

            return Result.Success();
        }

        public Maybe<Draft> GetDraft(ConfigurationId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Draft);
            if(dataRow == null)
            {
                return Maybe<Draft>.None;
            }

            return DraftFactory.Create(dataRow.Id, dataRow.Data, dataRow.DraftAuthor, dataRow.DraftCreation);
        }

        public Result Save(Planned planned)
        {
            var dataRow = _data.Single(d => d.Id == planned.Id.AsGuid());

            dataRow.Status = ConfigStatus.Planned;
            dataRow.PlannedAuthor = planned.Author.AsString();
            dataRow.PlannedCreation = planned.CreationDate.AsDateTime();
            dataRow.WhenGoLive = planned.WhenGoLive.AsDateTime();

            return Result.Success();
        }

        public Maybe<Planned> GetPlanned(ConfigurationId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Planned);
            if(dataRow == null)
            {
                return Maybe<Planned>.None;
            }

            return PlannedFactory.Create(dataRow.Id, dataRow.Data, dataRow.PlannedAuthor, dataRow.PlannedCreation, dataRow.WhenGoLive);
        }

        public Result Save(Live live)
        {
            var dataRow = _data.Single(d => d.Id == live.Id.AsGuid());

            dataRow.Status = ConfigStatus.Live;
            dataRow.LiveAuthor = live.Author.AsString();
            dataRow.LiveCreation = live.CreationDate.AsDateTime();

            return Result.Success();
        }

        public Maybe<Live> GetLive(ConfigurationId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Live);
            if (dataRow == null)
            {
                return Maybe<Live>.None;
            }

            return LiveFactory.Create(dataRow.Id, dataRow.Data, dataRow.LiveAuthor, dataRow.LiveCreation);
        }

        public Result Save(Archive archived)
        {
            var dataRow = _data.Single(d => d.Id == archived.Id.AsGuid());

            dataRow.Status = ConfigStatus.Archived;
            dataRow.ArchivedAuthor = archived.Author.AsString();
            dataRow.ArchiveCreation = archived.CreationDate.AsDateTime();

            return Result.Success();
        }

        public Maybe<Archive> GetArchived(ConfigurationId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Archived);
            if (dataRow == null)
            {
                return Maybe<Archive>.None;
            }
            return ArchiveFactory.Create(dataRow.Id, dataRow.Data, dataRow.ArchivedAuthor, dataRow.ArchiveCreation);
        }

        public Result DraftExists(ConfigurationId id)
        {
            return _data.Any(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Draft) 
                    ? Result.Success() 
                    : Result.Failure($"There is no draft with id: {id}");
        }

        public Result PlannedExists(ConfigurationId id)
        {
            return _data.Any(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Planned)
                    ? Result.Success()
                    : Result.Failure($"There is no planned with id {id}");
        }

        internal SampleDataTable Get(Guid id)
        {
            return _data.FirstOrDefault(d => d.Id == id);
        }
    }
}