using System;

using OperationResults.OperationErrors.Warnings;

namespace OperationResults
{
    internal interface IOperationResultInternal
    {
        internal static OperationResult GetSuccessResult() => new OperationResult(default(object));
    }

    internal interface IOperationResultInternal<TResult> : IOperationResultInternal
    {
        internal static new OperationResult GetSuccessResult() => new OperationResult<TResult>(default(TResult));
    }

    [Serializable]
    public class OperationResult : IOperationResultInternal
    {
        public static OperationResult Success { get; } = new();

        public bool IsSuccess => Error is null or Warning;
        public bool HasError => Error is not null and not Warning;
        public OperationError Error { get; }
        public object Value { get; }

        protected OperationResult() { }

        public OperationResult(OperationError error)
            => Error = error;

        public OperationResult(object value)
            => Value = value;

        public void Deconstruct(out object value, out OperationError error)
        {
            value = Value;
            error = Error;
        }

        public static OperationResult<T> FromValue<T>(T val)
            => new(val);

        public static OperationResult<T> FromValue<T>(object val)
            => new((T)val);

        public static OperationResult FromError(OperationError error)
            => new(error);

        public static OperationResult<T> FromError<T>(OperationError error)
            => new(error);

        public static implicit operator OperationResult(OperationError error)
            => new(error);

        public static bool operator !(OperationResult operationResult)
            => operationResult.HasError;
    }

    [Serializable]
    public class OperationResult<T> : OperationResult, IOperationResultInternal<T>
    {
        public new T Value { get; }

        public OperationResult(OperationError error) : base(error) { }

        public OperationResult(T value)
            => Value = value;

        public void Deconstruct(out T value, out OperationError error)
        {
            value = Value;
            error = Error;
        }

        public static implicit operator OperationResult<T>(T val)
            => new(val);

        public static implicit operator OperationResult<T>(OperationError error)
            => new(error);
    }
}
