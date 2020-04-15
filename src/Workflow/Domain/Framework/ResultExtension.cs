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