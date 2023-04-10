using System;
using System.Threading.Tasks;

using OperationResults.Extensions;
using OperationResults.OperationErrors;

namespace OperationResults.Workflow
{
    public static partial class WorkflowExtensions
    {
        public static Task<TResult> Execute<TResult>(Func<Task<TResult>> action)
            where TResult : OperationResult
            => action.Invoke();

        public static async Task<TResult> Execute<TSource, TResult>(this Task<TSource> source, Func<TSource, Task<TResult>> action)
            where TSource : OperationResult
            where TResult : OperationResult
            => await (await source).Execute<TSource, TResult>(action);

        public static Task<TResult> Execute<TSource, TResult>(this TSource source, Func<TSource, Task<TResult>> action)
            where TSource : OperationResult
            where TResult : OperationResult
            => action.Invoke(source);

        public static Task<TResult> SafeExecute<TResult, TError>(Func<Task<TResult>> action, string errorMessage = null)
            where TResult : OperationResult
            where TError : OperationError
            => OperationResult.Success.SafeExecute<OperationResult, TResult, TError, Exception>(_ => action.Invoke(), errorMessage);

        public static async Task<TResult> SafeExecute<TSource, TResult, TError>(this Task<TSource> source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            => await (await source).SafeExecute<TSource, TResult, TError>(action, errorMessage);

        public static Task<TResult> SafeExecute<TSource, TResult, TError>(this TSource source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            => source.SafeExecute<TSource, TResult, TError, Exception>(action, errorMessage);

        public static async Task<TResult> SafeExecute<TSource, TResult, TError, TException>(this Task<TSource> source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            where TException : Exception
            => await (await source).SafeExecute<TSource, TResult, TError, TException>(action, errorMessage);

        public static async Task<TResult> SafeExecute<TSource, TResult, TError, TException>(this TSource source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            where TException : Exception
        {
            try { return await action.Invoke(source); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>().WrapErrorWithTypeCheck<TResult, TError>(); }
        }

        public static async Task<TResult> ExecuteIfSuccess<TSource, TResult>(this Task<TSource> source, Func<TSource, Task<TResult>> action)
            where TSource : OperationResult
            where TResult : OperationResult
            => await (await source).ExecuteIfSuccess<TSource, TResult>(action);

        public static Task<TResult> ExecuteIfSuccess<TSource, TResult>(this TSource source, Func<TSource, Task<TResult>> action)
            where TSource : OperationResult
            where TResult : OperationResult
            => !source ? source.Error.WrapErrorWithTypeCheck<TResult, OperationError>().AsTask() : action.Invoke(source);

        public static Task<TResult> SafeExecuteIfSuccess<TSource, TResult, TError>(this Task<TSource> source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            => source.SafeExecuteIfSuccess<TSource, TResult, TError, Exception>(action, errorMessage);

        public static Task<TResult> SafeExecuteIfSuccess<TSource, TResult, TError>(this TSource source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            => source.SafeExecuteIfSuccess<TSource, TResult, TError, Exception>(action, errorMessage);

        public static async Task<TResult> SafeExecuteIfSuccess<TSource, TResult, TError, TException>(this Task<TSource> source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            where TException : Exception
            => await (await source).SafeExecuteIfSuccess<TSource, TResult, TError, TException>(action, errorMessage);

        public static async Task<TResult> SafeExecuteIfSuccess<TSource, TResult, TError, TException>(this TSource source, Func<TSource, Task<TResult>> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            where TException : Exception
        {
            if (!source)
                return source.Error.WrapErrorWithTypeCheck<TResult, OperationError>();

            try { return await action.Invoke(source); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>().WrapErrorWithTypeCheck<TResult, TError>(); }
        }
    }
}
