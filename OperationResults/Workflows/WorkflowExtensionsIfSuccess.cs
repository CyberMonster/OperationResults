using System;

namespace OperationResults.Workflow
{
    public static partial class WorkflowExtensions
    {
        public static OperationResult ExecuteIfSuccess(this OperationResult source, Action<OperationResult> action)
            => !source ? source : source.Execute(action);

        public static OperationResult ExecuteIfSuccess<TSource>(this OperationResult<TSource> source, Action<OperationResult<TSource>> action)
            => !source ? source : source.Execute(action);

        public static OperationResult<TResult> ExecuteIfSuccess<TResult>(this OperationResult<TResult> source, Func<OperationResult<TResult>, OperationResult<TResult>> action)
            => !source ? source : source.Execute(action);

        public static OperationResult SafeExecuteIfSuccess<TError>(this OperationResult source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            => !source ? source : source.SafeExecute<TError>(action, errorMessage);

        public static OperationResult SafeExecuteIfSuccess<TError, TException>(this OperationResult source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
            => !source ? source : source.SafeExecute<TError, TException>(action, errorMessage);

        public static OperationResult SafeExecuteIfSuccess<TSource, TError>(this OperationResult<TSource> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            => !source ? source : source.SafeExecute<TSource, TError>(action, errorMessage);

        public static OperationResult SafeExecuteIfSuccess<TSource, TError, TException>(this OperationResult<TSource> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
            => !source ? source : source.SafeExecute<TSource, TError, TException>(action, errorMessage);

        public static OperationResult<TResult> SafeExecuteIfSuccess<TResult, TError>(this OperationResult<TResult> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            => !source ? source : source.SafeExecute<TResult, TError>(action, errorMessage);

        public static OperationResult<TResult> SafeExecuteIfSuccess<TResult, TError, TException>(this OperationResult<TResult> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
            => !source ? source : source.SafeExecute<TResult, TError, TException>(action, errorMessage);
    }
}
