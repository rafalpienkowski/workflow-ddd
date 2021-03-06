using System;
using System.Linq;

namespace Workflow.Domain.Framework
{

    public struct Result<T>
    {
        private readonly T _value;

        public T Value => IsSuccess ? _value : throw new ArgumentNullException();
        public string Message { get; }
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        private Result(bool isFailure, T value, string message)
        {
            IsFailure = isFailure;
            _value = value;
            Message = message;
        }

        public static Result<T> Success(T value) => new Result<T>(false, value, "ok");
        public static Result<T> Failure(string message) => new Result<T>(true, default(T), message);
        public static implicit operator Result(Result<T> result)
        {
            if(result.IsSuccess)
            {
                return Result.Success();
            }

            return Result.Failure(result.Message);
        }

        public static implicit operator Maybe<T>(Result<T> result)
        {
            if (result.IsFailure)
            {
                return Maybe<T>.None;
            }

            return Maybe<T>.From(result.Value);
        }
    }
}