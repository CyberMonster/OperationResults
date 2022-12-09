using System;
using System.Threading.Tasks;

namespace OperationResults.Extensions
{
    public static class AsyncOperationResultExtensions
    {
        public static async Task<T> UnwrapValue<T>(this Task<OperationResult<T>> operationResultTask)
            => (await operationResultTask).UnwrapValue();

        public static async Task<OperationResult> ThrowIfError(this Task<OperationResult> operationResultTask)
            => (await operationResultTask).ThrowIfError();

        public static async Task<OperationResult<T>> ThrowIfError<T>(this Task<OperationResult<T>> operationResultTask)
            => (await operationResultTask).ThrowIfError();

        public static async Task<T> UnwrapValueIgnoreError<T>(this Task<OperationResult<T>> operationResultTask)
            => (await operationResultTask).UnwrapValueIgnoreError();

        public static async Task<OperationResult<U>> DoActionSafe<T, U>(this Task<OperationResult<T>> operationResultTask, Func<T, U> action)
            => (await operationResultTask).DoActionSafe(action);

        public static async Task<OperationResult> DoActionSafe<T>(this Task<OperationResult<T>> operationResultTask, Action<T> action)
            => (await operationResultTask).DoActionSafe(action);

        public static async Task<OperationResult<T>> SetValueSafe<T>(this Task<OperationResult> operationResultTask, T value)
            => (await operationResultTask).SetValueSafe(value);

        public static async Task<OperationResult<T>> LogErrorIfNeeded<T>(this Task<OperationResult<T>> operationResultTask, Action<OperationError> logDelegate)
            => (await operationResultTask).LogErrorIfNeeded(logDelegate);

        public static async Task<OperationResult> LogErrorIfNeeded(this Task<OperationResult> operationResultTask, Action<OperationError> logDelegate)
            => (await operationResultTask).LogErrorIfNeeded(logDelegate);
    }
}
