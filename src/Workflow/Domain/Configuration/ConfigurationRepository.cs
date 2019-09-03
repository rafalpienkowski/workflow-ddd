using System;
using System.Collections.Generic;
using System.Linq;

namespace Workflow.Domain.Configuration
{
    public class ConfigurationRepository
    {
        private readonly List<SampleDataTable> _data = new List<SampleDataTable>();
        
        public int Save(Draft draft)
        {
            var id = _data.Count + 1;
            var dataRow = new SampleDataTable
            {
                Id = id,
                Status = ConfigStatus.Draft,
                Data = draft.Data,
                DraftCreation = draft.CreationDate,
                DraftAuthor = draft.Author
            };
            _data.Add(dataRow);
            
            return dataRow.Id;
        }

        public Draft GetDraft(int id)
        {
            var dataRow = _data.FirstOrDefault(d => d.Id == id && d.Status == ConfigStatus.Draft);
            return dataRow == null ? null : new Draft(dataRow.Id, dataRow.Data, dataRow.DraftAuthor, dataRow.DraftCreation);
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
            return dataRow == null ? null : new Planned(dataRow.Id, dataRow.Data, dataRow.PlannedAuthor, dataRow.PlannedCreation, dataRow.WhenGoLive);
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
            return dataRow == null ? null : new Live(dataRow.Id, dataRow.Data, dataRow.LiveAuthor, dataRow.LiveCreation);
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
            return dataRow == null ? null : new Archived(dataRow.Id, dataRow.Data, dataRow.ArchivedAuthor, dataRow.ArchiveCreation);
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

    internal class SampleDataTable
    {
        public int Id { get; set; }
        public ConfigStatus Status { get; set; }
        public string Data { get; set; }
        public string DraftAuthor { get; set; }
        public DateTime DraftCreation { get; set; }
        public string PlannedAuthor { get; set; }
        public DateTime PlannedCreation { get; set; }
        public DateTime WhenGoLive { get; set; }
        public string LiveAuthor { get; set; }
        public DateTime LiveCreation { get; set; }
        public string ArchivedAuthor { get; set; }
        public DateTime ArchiveCreation { get; set; }
    }

    internal enum ConfigStatus
    {
        Draft = 1,
        Planned = 2,
        Live = 3,
        Archived = 4
    }
}