using System;
using System.Threading.Tasks;

namespace OperationResults.Extensions
{
    public static class AsyncOperationResultExtensions
    {
        public static async Task<T> UnwrapValue<T>(this Task<OperationResult<T>> operationResultTask)
            => (await operationResultTask).UnwrapValue();

        public static async Task<TResult> ThrowIfError<TResult>(this Task<TResult> operationResultTask) where TResult : OperationResult
            => (await operationResultTask).ThrowIfError();

        public static async Task<T> UnwrapValueIgnoreError<T>(this Task<OperationResult<T>> operationResultTask)
            => (await operationResultTask).UnwrapValueIgnoreError();

        public static async Task<OperationResult<T>> SetValueSafe<T>(this Task<OperationResult> operationResultTask, T value)
            => (await operationResultTask).SetValueSafe(value);

        public static async Task<TResult> LogErrorIfNeeded<TResult>(this Task<TResult> operationResult, Action logDelegate) where TResult : OperationResult
            => (await operationResult).LogErrorIfNeeded(logDelegate);

        public static async Task<TResult> LogErrorIfNeeded<TResult>(this Task<TResult> operationResult, Action<OperationError> logDelegate) where TResult : OperationResult
            => (await operationResult).LogErrorIfNeeded(logDelegate);
    }
}
