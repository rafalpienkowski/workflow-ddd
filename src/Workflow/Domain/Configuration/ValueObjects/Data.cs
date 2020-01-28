
namespace Workflow.Domain.Configuration.ValueObjects
{
    /// <summary>
    /// Data which are stored in our workflow
    /// </summary>
    public class Data
    {
        private readonly string _data;

        private Data(string data)
        {
            //TODO validation
            _data = data;
        }

        public static Data FromString(string data) => new Data(data);

        public string AsString() => _data;
    }
}