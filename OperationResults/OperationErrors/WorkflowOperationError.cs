using System;

namespace OperationResults.OperationErrors
{
    internal sealed class WorkflowOperationError : OperationError
    {
        public WorkflowOperationError(Exception innerException, string message = null) : base(innerException, message) { }
    }
}
