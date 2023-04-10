using System;

namespace OperationResults.OperationErrors.Warnings
{
    public class Warning : OperationError
    {
        public Warning(string message = null)
            => Message = message;

        public Warning(OperationError innerError, string message = null)
        {
            InnerError = innerError;
            Message = message;
        }

        public Warning(Exception innerException, string message = null)
        {
            InnerException = innerException;
            Message = message;
        }
    }
}
