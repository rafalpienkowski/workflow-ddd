using System;

namespace Workflow.Tests
{
    internal class SampleDataTable
    {
        public Guid Id { get; set; }
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
}