using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Workflow.Tests")]
namespace Workflow.Domain.Configuration.ValueObjects
{
    public class ConfigurationId
    {
        private readonly Guid _id;

        private ConfigurationId(Guid id)
        {
            _id = id;
        }

        internal static ConfigurationId FromGuid(Guid id) => new ConfigurationId(id);

        public static ConfigurationId New() => FromGuid(Guid.NewGuid());

        public Guid AsGuid() => _id;
    }
}