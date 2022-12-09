using System;

namespace OperationResults
{
    [Serializable]
    public class OperationResult
    {
        public static OperationResult Success => new();

        public bool IsSuccess => Error is null;
        public bool HasError => Error is not null;
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
    public class OperationResult<T> : OperationResult
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
