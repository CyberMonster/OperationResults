using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OperationResults.Extensions
{
    public static class OperationErrorExtensions
    {
        private static readonly BindingFlags HiddenMemberSearchFlags = BindingFlags.GetField
            | BindingFlags.GetProperty
            | BindingFlags.Instance
            | BindingFlags.NonPublic
            | BindingFlags.Public;

        public static T CastToType<T>(this OperationError error) where T : OperationError
        {
            T result;
            var destinationType = typeof(T);
            var defaultCtor = destinationType.GetConstructor(HiddenMemberSearchFlags, null, Type.EmptyTypes, null);
            var messageCtor = destinationType.GetConstructor(HiddenMemberSearchFlags, null, new Type[] { typeof(string) }, null);
            if (messageCtor is not null)
                result = (T)messageCtor.Invoke(new[] { error.Message });
            else
                result = (T)defaultCtor.Invoke(null);

            destinationType.GetProperty(nameof(OperationError.InnerException)).SetValue(result, error.InnerException, null);
            destinationType.GetProperty(nameof(OperationError.InnerError)).SetValue(result, error.InnerError, null);

            return result;
        }

        public static OperationResult<T> RemoveInnerException<T>(this OperationError error)
            => ((OperationResult<T>)error).RemoveInnerException();

        public static OperationResult<T> RemoveInnerException<T>(this OperationResult<T> result)
        {
            if (result.Error?.InnerException is not null)
            {
                var error = result.Error;
                var innerExceptionProperty = error.GetType()
                    .GetMembers()
                    .Where(x => x.Name == nameof(OperationError.InnerException)).FirstOrDefault() as PropertyInfo;
                innerExceptionProperty.SetValue(result.Error, null, null);
            }

            return result;
        }

        public static T LogErrorIfNeeded<T>(this T error, Action<T> logDelegate) where T : OperationError
        {
            if (!error && !error.IsLogged)
            {
                logDelegate.Invoke(error);
                error.IsLogged = true;
            }

            return error;
        }

        public static T LogErrorIfNeeded<T>(this T error, Action logDelegate) where T : OperationError
        {
            if (!error && !error.IsLogged)
            {
                logDelegate.Invoke();
                error.IsLogged = true;
            }

            return error;
        }

        public static Task<OperationResult> AsTask(this OperationError error)
            => Task.FromResult(OperationResult.FromError(error));

        public static Task<OperationResult<T>> AsTask<T>(this OperationError error)
            => Task.FromResult(OperationResult.FromError<T>(error));
    }
}
