using System;
using System.Linq;

using OperationResults.Extensions;
using OperationResults.OperationErrors;

namespace OperationResults.Workflow
{
    public static partial class WorkflowExtensions
    {
        public static TResult Execute<TResult>(Func<TResult> action)
            where TResult : OperationResult
            => action.Invoke();

        public static TResult Execute<TSource, TResult>(this TSource source, Func<TSource, TResult> action)
            where TSource : OperationResult
            where TResult : OperationResult
            => action.Invoke(source);

        public static TResult SafeExecute<TResult, TError>(Func<TResult> action, string errorMessage = null)
            where TResult : OperationResult
            where TError : OperationError
            => OperationResult.Success.SafeExecute<OperationResult, TResult, TError, Exception>(_ => action.Invoke(), errorMessage);

        public static TResult SafeExecute<TSource, TResult, TError>(this TSource source, Func<TSource, TResult> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            => source.SafeExecute<TSource, TResult, TError, Exception>(action, errorMessage);

        public static TResult SafeExecute<TSource, TResult, TError, TException>(this TSource source, Func<TSource, TResult> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            where TException : Exception
        {
            try { return action.Invoke(source); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>().WrapErrorWithTypeCheck<TResult, TError>(); }
        }

        public static TResult ExecuteIfSuccess<TSource, TResult>(this TSource source, Func<TSource, TResult> action)
            where TSource : OperationResult
            where TResult : OperationResult
            => !source ? source.Error.WrapErrorWithTypeCheck<TResult, OperationError>() : action.Invoke(source);

        public static TResult SafeExecuteIfSuccess<TSource, TResult, TError>(this TSource source, Func<TSource, TResult> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            => source.SafeExecuteIfSuccess<TSource, TResult, TError, Exception>(action, errorMessage);

        public static TResult SafeExecuteIfSuccess<TSource, TResult, TError, TException>(this TSource source, Func<TSource, TResult> action, string errorMessage = null)
            where TSource : OperationResult
            where TResult : OperationResult
            where TError : OperationError
            where TException : Exception
        {
            if (!source)
                return source.Error.WrapErrorWithTypeCheck<TResult, OperationError>();

            try { return action.Invoke(source); }
            catch (TException ex) { return new WorkflowOperationError(ex, errorMessage).CastToType<TError>().WrapErrorWithTypeCheck<TResult, TError>(); }
        }

        private static TResult WrapErrorWithTypeCheck<TResult, TError>(this TError error)
            where TResult : OperationResult
            where TError : OperationError
        {
            try
            {
                var neededType = typeof(TResult);
                var neededCtor = neededType.GetConstructors().FirstOrDefault(ctor =>
                {
                    var ctor_Params = ctor.GetParameters();
                    return ctor_Params.Length == 1 && ctor_Params[0].ParameterType == typeof(OperationError);
                });
                if (neededCtor is not null)
                    return (TResult)neededCtor.Invoke(new[] { error });
                else
                    return (TResult)error;
            }
            catch
            {
                throw new InvalidCastException($"Can't cast source error to destination wrapper. Source type: {typeof(TError).FullName} Destination type: {typeof(TResult).FullName}", error.AsException());
            }
        }
    }
}
