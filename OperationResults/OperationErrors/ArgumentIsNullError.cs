using System;

namespace OperationResults.OperationErrors
{
    public class ArgumentIsNullError : OperationError
    {
        private ArgumentIsNullError(string message = null) : base(message) { }
        public ArgumentIsNullError(string argumentName, Type argumentType = null) : base($"Argument is null. Argument name: {argumentName}{(argumentType is not null ? $" Argument type: {argumentType.FullName}" : string.Empty)}") { }
    }
}
