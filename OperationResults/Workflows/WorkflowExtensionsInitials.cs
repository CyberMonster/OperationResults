using System;
using System.Threading.Tasks;

using OperationResults.Extensions;
using OperationResults.OperationErrors;

namespace OperationResults.Workflow
{
    public static partial class WorkflowExtensions
    {
        public static OperationResult Execute(Func<OperationResult> action)
            => action.Invoke();

        public static OperationResult<TResult> Execute<TResult>(Func<OperationResult<TResult>> action)
            => action.Invoke();

        public static OperationResult SafeExecute<TError>(Func<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            => SafeExecute<TError, Exception>(action, errorMessage);

        public static OperationResult SafeExecute<TError, TException>(Func<OperationResult> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }
        }

        public static OperationResult<TResult> SafeExecute<TResult, TError>(Func<OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            => SafeExecute<TResult, TError, Exception>(action, errorMessage);

        public static OperationResult<TResult> SafeExecute<TResult, TError, TException>(Func<OperationResult<TResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>(); }
        }
    }
}
