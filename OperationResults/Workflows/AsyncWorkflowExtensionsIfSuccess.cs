using System;
using System.Threading.Tasks;

namespace OperationResults.Workflow
{
    public static partial class AsyncWorkflowExtensions
    {
        public static async Task<OperationResult> ExecuteIfSuccess(this Task<OperationResult> source, Action<OperationResult> action)
        {
            var result = await source;
            return !result ? result : result.Execute(action);
        }

        public static async Task<OperationResult> ExecuteIfSuccess<TSource>(this Task<OperationResult<TSource>> source, Action<OperationResult<TSource>> action)
        {
            var result = await source;
            return !result ? result : result.Execute(action);
        }

        public static async Task<OperationResult<TResult>> ExecuteIfSuccess<TResult>(this Task<OperationResult<TResult>> source, Func<OperationResult<TResult>, OperationResult<TResult>> action)
        {
            var result = await source;
            return !result ? result : result.Execute(action);
        }

        public static async Task<OperationResult> SafeExecuteIfSuccess<TError>(this Task<OperationResult> source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
        {
            var result = await source;
            return !result ? result : result.SafeExecute<TError>(action, errorMessage);
        }

        public static async Task<OperationResult> SafeExecuteIfSuccess<TError, TException>(this Task<OperationResult> source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            var result = await source;
            return !result ? result : result.SafeExecute<TError, TException>(action, errorMessage);
        }

        public static async Task<OperationResult> SafeExecuteIfSuccess<TSource, TError>(this Task<OperationResult<TSource>> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
        {
            var result = await source;
            return !result ? result : result.SafeExecute<TSource, TError>(action, errorMessage);
        }

        public static async Task<OperationResult> SafeExecuteIfSuccess<TSource, TError, TException>(this Task<OperationResult<TSource>> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            var result = await source;
            return !result ? result : result.SafeExecute<TSource, TError, TException>(action, errorMessage);
        }

        public static async Task<OperationResult<TResult>> SafeExecuteIfSuccess<TResult, TError>(this Task<OperationResult<TResult>> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
        {
            var result = await source;
            return !result ? result : result.SafeExecute<TResult, TError>(action, errorMessage);
        }

        public static async Task<OperationResult<TResult>> SafeExecuteIfSuccess<TResult, TError, TException>(this Task<OperationResult<TResult>> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            var result = await source;
            return !result ? result : result.SafeExecute<TResult, TError, TException>(action, errorMessage);
        }
    }
}
