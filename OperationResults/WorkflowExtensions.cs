using System;

using OperationResults.Extensions;
using OperationResults.OperationErrors;

namespace OperationResults
{
    public static class WorkflowExtensions
    {
        public static OperationResult Execute(Func<OperationResult> action)
            => action.Invoke();

        public static OperationResult<TResult> Execute<TResult>(Func<OperationResult<TResult>> action)
            => action.Invoke();

        public static OperationResult Execute(this OperationResult source, Action<OperationResult> action)
        {
            action.Invoke(source);
            return OperationResult.Success;
        }

        public static OperationResult Execute<TSource>(this OperationResult<TSource> source, Action<OperationResult<TSource>> action)
        {
            action.Invoke(source);
            return OperationResult.Success;
        }

        public static OperationResult<TResult> Execute<TResult>(this OperationResult<TResult> source, Func<OperationResult<TResult>, OperationResult<TResult>> action)
            => action.Invoke(source);

        public static OperationResult SafeExecute<TError, TException>(Func<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }
        }

        public static OperationResult<TResult> SafeExecute<TResult, TError, TException>(Func<OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }
        }

        public static OperationResult SafeExecute<TError>(this OperationResult source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            => source.SafeExecute<TError, Exception>(action, errorMessage);

        public static OperationResult SafeExecute<TError, TException>(this OperationResult source, Action<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { source.Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }

            return OperationResult.Success;
        }

        public static OperationResult SafeExecute<TSource, TError>(this OperationResult<TSource> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            => source.SafeExecute<TSource, TError, Exception>(action, errorMessage);

        public static OperationResult SafeExecute<TSource, TError, TException>(this OperationResult<TSource> source, Action<OperationResult<TSource>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { source.Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }

            return OperationResult.Success;
        }

        public static OperationResult<TResult> SafeExecute<TResult, TError>(this OperationResult<TResult> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            => source.SafeExecute<TResult, TError, Exception>(action, errorMessage);

        public static OperationResult<TResult> SafeExecute<TResult, TError, TException>(this OperationResult<TResult> source, Func<OperationResult<TResult>, OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return source.Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }
        }
    }
}
