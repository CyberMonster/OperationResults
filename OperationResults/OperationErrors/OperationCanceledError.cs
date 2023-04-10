using System.Threading;

namespace OperationResults.OperationErrors
{
    public class OperationCanceledError : OperationError
    {
        private OperationCanceledError() { }
        public OperationCanceledError(CancellationToken? token = null) : base($"Operation canceled by cancellation token. Token: {token?.GetHashCode() ?? 0}") { }
    }
}
