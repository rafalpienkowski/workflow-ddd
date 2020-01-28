using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Domain.Configuration;
using Workflow.Domain.Configuration.Entities;
using Workflow.Domain.Configuration.ValueObjects;

namespace Workflow.Tests
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly List<SampleDataTable> _data = new List<SampleDataTable>();

        public void Save(Draft draft)
        {
            var dataRow = new SampleDataTable
            {
                Id = draft.Id.AsInt(),
                Status = ConfigStatus.Draft,
                Data = draft.Data.ToString(),
                DraftCreation = draft.CreationDate.AsDateTime(),
                DraftAuthor = draft.Author.ToString()
            };
            _data.Add(dataRow);
        }

        public Draft GetDraft(DraftId id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id.AsInt() && d.Status == ConfigStatus.Draft);
            if(dataRow == null)
            {
                return null;
            }

            return DraftFactory.Create(Guid.NewGuid(), dataRow.Data, dataRow.DraftAuthor, dataRow.DraftCreation);
        }

        public int Save(Planned planned)
        {
            var dataRow = _data.Single(d => d.Id == planned.Id);

            dataRow.Status = ConfigStatus.Planned;
            dataRow.PlannedAuthor = planned.Author;
            dataRow.PlannedCreation = planned.CreationDate;
            dataRow.WhenGoLive = planned.WhenGoLive;

            return dataRow.Id;
        }

        public Planned GetPlanned(int id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id && d.Status == ConfigStatus.Planned);
            return dataRow == null
                ? null
                : new Planned(dataRow.Id, dataRow.Data, dataRow.PlannedAuthor, dataRow.PlannedCreation,
                    dataRow.WhenGoLive);
        }

        public int Save(Live live)
        {
            var dataRow = _data.Single(d => d.Id == live.Id);

            dataRow.Status = ConfigStatus.Live;
            dataRow.LiveAuthor = live.Author;
            dataRow.LiveCreation = live.CreationDate;

            return dataRow.Id;
        }

        public Live GetLive(int id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id && d.Status == ConfigStatus.Live);
            return dataRow == null
                ? null
                : new Live(dataRow.Id, dataRow.Data, dataRow.LiveAuthor, dataRow.LiveCreation);
        }

        public int Save(Archived archived)
        {
            var dataRow = _data.Single(d => d.Id == archived.Id);

            dataRow.Status = ConfigStatus.Archived;
            dataRow.ArchivedAuthor = archived.Author;
            dataRow.ArchiveCreation = archived.CreationDate;

            return dataRow.Id;
        }

        public Archived GetArchived(int id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id && d.Status == ConfigStatus.Archived);
            return dataRow == null
                ? null
                : new Archived(dataRow.Id, dataRow.Data, dataRow.ArchivedAuthor, dataRow.ArchiveCreation);
        }

        public bool DraftExists(int id)
        {
            return _data.Any(d => d.Id == id && d.Status == ConfigStatus.Draft);
        }

        public bool PlannedExists(int id)
        {
            return _data.Any(d => d.Id == id && d.Status == ConfigStatus.Planned);
        }

        internal SampleDataTable Get(int id)
        {
            return _data.FirstOrDefault(d => d.Id == id);
        }
    }
}