using System;

namespace OperationResults.OperationErrors
{
    public class ArgumentIsNullOrWhiteSpaceError : OperationError
    {
        private ArgumentIsNullOrWhiteSpaceError(string message = null) : base(message) { }
        public ArgumentIsNullOrWhiteSpaceError(string argumentName, Type argumentType = null) : base($"Argument is null or white space. Argument name: {argumentName}{(argumentType is not null ? $" Argument type: {argumentType.FullName}" : string.Empty)}") { }
    }
}
