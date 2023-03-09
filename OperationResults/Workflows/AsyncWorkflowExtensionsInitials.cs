using System;
using System.Threading.Tasks;

using OperationResults.Extensions;
using OperationResults.OperationErrors;

namespace OperationResults.Workflow
{
    public static partial class AsyncWorkflowExtensions
    {
        public static Task<OperationResult> Execute(Func<Task<OperationResult>> action)
            => action.Invoke();

        public static Task<OperationResult<TResult>> Execute<TResult>(Func<Task<OperationResult<TResult>>> action)
            => action.Invoke();

        public static Task<OperationResult> SafeExecute<TError, TException>(Func<Task<OperationResult>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>().AsTask(); }
        }

        public static Task<OperationResult<TResult>> SafeExecute<TResult, TError, TException>(Func<Task<OperationResult<TResult>>> action, string errorMessage = null)
            where TError : OperationError
            where TException : Exception
        {
            try { return Execute(action); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>().AsTask<TResult>(); }
        }
    }
}
