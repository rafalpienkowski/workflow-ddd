using Workflow.Domain.Framework;

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
            _data = data;
        }

        public static Data FromString(string data)
        {
            if (data.Length > 250)
            {
                throw new BusinessException("Only short data are supported");
            }

            return new Data(data);
        }

        public string AsString() => _data;
    }
}