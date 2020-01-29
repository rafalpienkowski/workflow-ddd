using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.ValueObjects
{
    public class ArchiveId
    {
        private readonly Guid _id;

        private ArchiveId(Guid id)
        {
            _id = id;
        }

        internal static ArchiveId FromGuid(Guid id) => new ArchiveId(id);

        public static ArchiveId FromLiveId(LiveId liveId) => new ArchiveId(liveId.AsGuid());

        public Guid AsGuid() => _id;
    }
}