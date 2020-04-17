using System;

namespace Workflow.Domain.Framework
{
    public static class ResultExtension
    {
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                return result;
            }

            action();

            return Result.Success();
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<Result<T>> action)
        {
            if (result.IsSuccess)
            {
                action(result);
            }

            return result;
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Func<Result<T>, Result<T>> func)
        {
            if (result.IsSuccess)
            {
                return func(result);
            }

            return result;
        }

        public static Result<T> OnBoth<T>(this Result<T> result, Func<Result<T>, Result<T>> func)
        {
            return func(result);
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Func<Result, Result> func)
        {
            if (result.IsSuccess)
            {
                return result;
            }

            return func(result);
        }
    }
}