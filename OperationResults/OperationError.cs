using System;
using System.Diagnostics;
using System.Text;

namespace OperationResults
{
    public interface IOperationError
    {
        public string Message { get; }
        public OperationError InnerError { get; }
        public Exception InnerException { get; }
        public StackTrace StackTrace { get; }
        public bool IsLogged { get; set; }

        public Exception AsException();
    }

    [Serializable]
    public abstract class OperationError : IOperationError
    {
        public virtual string Message { get; protected set; }
        public OperationError InnerError { get; protected set; }
        public Exception InnerException { get; protected set; }

        private readonly StackTrace _stackTrace = new();
        public StackTrace StackTrace => _stackTrace;

        public bool IsLogged { get; set; }

        /// <summary>
        /// Default .ctor. All derived classes must have .ctor with this signature or new().
        /// Some internal methods use it for work (<see cref="Extensions.OperationErrorExtensions.CastToType{T}(OperationError)"/>)!
        /// </summary>
        /// <param name="message"></param>
        protected OperationError(string message = null)
            => Message = message;

        protected OperationError(OperationError innerError, string message = null)
        {
            InnerError = innerError;
            Message = message;
        }

        protected OperationError(Exception innerException, string message = null)
        {
            InnerException = innerException;
            Message = message;
        }

        public static bool operator !(OperationError operationError)
            => operationError is not null;

        public virtual Exception AsException()
            => new($"{GetType().Name}: {Message}", InnerException ?? InnerError?.AsException());

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType().Name).Append(": ").Append(Message);

            if (InnerException is not null)
                builder.AppendLine().AppendLine("---").Append(InnerException.ToString());
            if (InnerError is not null)
                builder.AppendLine().AppendLine("---").Append(InnerError.ToString());
            if (StackTrace is not null)
                builder.AppendLine().AppendLine("---").Append(StackTrace.ToString());

            return builder.ToString();
        }
    }
}
