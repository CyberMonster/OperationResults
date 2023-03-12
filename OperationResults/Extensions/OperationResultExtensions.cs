using System;
using System.Threading.Tasks;

namespace OperationResults.Extensions
{
    public static class OperationResultExtensions
    {
        public static T UnwrapValue<T>(this OperationResult<T> operationResult)
            => operationResult.ThrowIfError().Value;

        public static TResult ThrowIfError<TResult>(this TResult operationResult) where TResult : OperationResult
        {
            if (operationResult.HasError)
                throw operationResult.Error.AsException();

            return operationResult;
        }

        public static T UnwrapValueIgnoreError<T>(this OperationResult<T> operationResult)
            => operationResult.Value;

        public static OperationResult<T> SetValueSafe<T>(this OperationResult operationResult, T value)
        {
            if (operationResult.HasError)
                return operationResult.Error;

            return OperationResult.FromValue(value);
        }

        public static TResult LogErrorIfNeeded<TResult>(this TResult operationResult, Action logDelegate) where TResult : OperationResult
        {
            operationResult.Error.LogErrorIfNeeded(logDelegate);
            return operationResult;
        }

        public static TResult LogErrorIfNeeded<TResult>(this TResult operationResult, Action<OperationError> logDelegate) where TResult : OperationResult
        {
            operationResult.Error.LogErrorIfNeeded(logDelegate);
            return operationResult;
        }

        public static Task<TResult> AsTask<TResult>(this TResult result) where TResult : OperationResult
            => Task.FromResult(result);
    }
}
