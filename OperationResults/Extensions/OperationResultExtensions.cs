using System;
using System.Threading.Tasks;

namespace OperationResults.Extensions
{
    public static class OperationResultExtensions
    {
        public static T UnwrapValue<T>(this OperationResult<T> operationResult)
            => operationResult.HasError
                ? throw operationResult.Error.AsException()
                : operationResult.Value;

        public static OperationResult ThrowIfError(this OperationResult operationResult)
        {
            if (operationResult.HasError)
                throw operationResult.Error.AsException();

            return operationResult;
        }

        public static OperationResult<T> ThrowIfError<T>(this OperationResult<T> operationResult)
        {
            if (operationResult.HasError)
                throw operationResult.Error.AsException();

            return operationResult;
        }

        public static T UnwrapValueIgnoreError<T>(this OperationResult<T> operationResult)
            => operationResult.Value;

        public static OperationResult<U> DoActionSafe<T, U>(this OperationResult<T> operationResult, Func<T, U> action)
            => operationResult.HasError ? operationResult.Error : action.Invoke(operationResult.Value);

        public static OperationResult DoActionSafe<T>(this OperationResult<T> operationResult, Action<T> action)
        {
            if (operationResult.HasError)
                return operationResult.Error;

            action(operationResult.Value);
            return OperationResult.Success;
        }

        public static OperationResult<T> SetValueSafe<T>(this OperationResult operationResult, T value)
        {
            if (operationResult.HasError)
                return operationResult.Error;

            return OperationResult.FromValue(value);
        }

        public static OperationResult<T> LogErrorIfNeeded<T>(this OperationResult<T> operationResult, Action<OperationError> logDelegate)
        {
            (operationResult as OperationResult).LogErrorIfNeeded(logDelegate);
            return operationResult;
        }

        public static OperationResult LogErrorIfNeeded(this OperationResult operationResult, Action<OperationError> logDelegate)
        {
            operationResult.Error.LogErrorIfNeeded(logDelegate);
            return operationResult;
        }

        public static OperationResult<T> LogErrorIfNeeded<T>(this OperationResult<T> operationResult, Action logDelegate)
        {
            (operationResult as OperationResult).LogErrorIfNeeded(logDelegate);
            return operationResult;
        }

        public static OperationResult LogErrorIfNeeded(this OperationResult operationResult, Action logDelegate)
        {
            operationResult.Error.LogErrorIfNeeded(logDelegate);
            return operationResult;
        }

        public static Task<OperationResult> AsTask(this OperationResult result)
            => Task.FromResult(result);

        public static Task<OperationResult<T>> AsTask<T>(this OperationResult<T> result)
            => Task.FromResult(result);
    }
}
