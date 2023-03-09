using System;
using System.Threading.Tasks;

namespace OperationResults.Workflow
{
    public static partial class AsyncWorkflowExtensions
    {
        public static async Task<OperationResult> Execute(this Task<OperationResult> source, Action<OperationResult> action)
            => (await source).Execute(action);

        public static async Task<OperationResult> Execute<TSource>(this Task<OperationResult<TSource>> source, Action<OperationResult<TSource>> action)
            => (await source).Execute(action);

        public static async Task<OperationResult<TResult>> Execute<TResult>(this Task<OperationResult<TResult>> source, Func<OperationResult<TResult>, OperationResult<TResult>> action)
            => (await source).Execute(action);

        public static async Task<OperationResult> SafeExecute<TError>(this Task<OperationResult> source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            => (await source).SafeExecute<TError>(action, errorMessage);

        public static async Task<OperationResult> SafeExecute<TError, TException>(this Task<OperationResult> source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
            => (await source).SafeExecute<TError, TException>(action, errorMessage);

        public static async Task<OperationResult> SafeExecute<TSource, TError>(this Task<OperationResult<TSource>> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            => (await source).SafeExecute<TSource, TError>(action, errorMessage);

        public static async Task<OperationResult> SafeExecute<TSource, TError, TException>(this Task<OperationResult<TSource>> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
            => (await source).SafeExecute<TSource, TError, TException>(action, errorMessage);

        public static async Task<OperationResult<TResult>> SafeExecute<TResult, TError>(this Task<OperationResult<TResult>> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            => (await source).SafeExecute<TResult, TError>(action, errorMessage);

        public static async Task<OperationResult<TResult>> SafeExecute<TResult, TError, TException>(this Task<OperationResult<TResult>> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
            => (await source).SafeExecute<TResult, TError, TException>(action, errorMessage);
    }
}
