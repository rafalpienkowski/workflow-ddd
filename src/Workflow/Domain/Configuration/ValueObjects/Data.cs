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

        public static Result<Data> FromString(string data)
        {
            if (data.Length > 250)
            {
                return Result<Data>.Failure("Only short data are supported");
            }

            return Result<Data>.Success(new Data(data));
        }

        public string AsString() => _data;
    }
}