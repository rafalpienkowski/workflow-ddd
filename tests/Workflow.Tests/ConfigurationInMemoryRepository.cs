using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.Factories;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Tests
{
    public class ConfigurationInMemoryRepository : IConfigurationRepository
    {
        private readonly List<SampleDataTable> _data = new List<SampleDataTable>();

        public void Save(Draft draft)
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
        }

        public Draft GetDraft(DraftId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Draft);
            if(dataRow == null)
            {
                return null;
            }

            return DraftFactory.Create(dataRow.Id, dataRow.Data, dataRow.DraftAuthor, dataRow.DraftCreation);
        }

        public void Save(Planned planned)
        {
            var dataRow = _data.Single(d => d.Id == planned.Id.AsGuid());

            dataRow.Status = ConfigStatus.Planned;
            dataRow.PlannedAuthor = planned.Author.AsString();
            dataRow.PlannedCreation = planned.CreationDate.AsDateTime();
            dataRow.WhenGoLive = planned.WhenGoLive.AsDateTime();
        }

        public Planned GetPlanned(PlannedId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Planned);
            if(dataRow == null)
            {
                return null;
            }

            return PlannedFactory.Create(dataRow.Id, dataRow.Data, dataRow.PlannedAuthor, dataRow.PlannedCreation, dataRow.WhenGoLive);
        }

        public void Save(Live live)
        {
            var dataRow = _data.Single(d => d.Id == live.Id.AsGuid());

            dataRow.Status = ConfigStatus.Live;
            dataRow.LiveAuthor = live.Author.AsString();
            dataRow.LiveCreation = live.CreationDate.AsDateTime();
        }

        public Live GetLive(LiveId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Live);
            if (dataRow == null)
            {
                return null;
            }

            return LiveFactory.Create(dataRow.Id, dataRow.Data, dataRow.LiveAuthor, dataRow.LiveCreation);
        }

        public void Save(Archive archived)
        {
            var dataRow = _data.Single(d => d.Id == archived.Id.AsGuid());

            dataRow.Status = ConfigStatus.Archived;
            dataRow.ArchivedAuthor = archived.Author.AsString();
            dataRow.ArchiveCreation = archived.CreationDate.AsDateTime();
        }

        public Archive GetArchived(ArchiveId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Archived);
            if (dataRow == null)
            {
                return null;
            }
            return ArchiveFactory.Create(dataRow.Id, dataRow.Data, dataRow.ArchivedAuthor, dataRow.ArchiveCreation);
        }

        public bool DraftExists(DraftId id)
        {
            return _data.Any(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Draft);
        }

        public bool PlannedExists(PlannedId id)
        {
            return _data.Any(d => d.Id == id.AsGuid() && d.Status == ConfigStatus.Planned);
        }

        internal SampleDataTable Get(Guid id)
        {
            return _data.FirstOrDefault(d => d.Id == id);
        }
    }
}