using System.Linq;

namespace Workflow.Domain.Framework
{
    public struct Result
    {
        public string Message { get; }
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        private Result(bool isFailure, string message)
        {
            IsFailure = isFailure;
            Message = message;
        }

        public static Result Success() => new Result(false, "ok");
        public static Result Failure(string message) => new Result(true, message);

        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result<T> Failure<T>(string message) => Result<T>.Failure(message);

        public static Result Combine(params Result[] results)
        {
            if(results.Any(r => r.IsFailure))
            {
                return Failure(string.Join(';', results.Select(r => r.Message)));
            }

            return Success();
        }
    }
}