using System.Collections;

namespace OperationResults.OperationErrors
{
    public class CollectionIsNullOrEmptyError<TCollection> : OperationError where TCollection : IEnumerable
    {
        private CollectionIsNullOrEmptyError(string message = null) : base(message) { }
        public CollectionIsNullOrEmptyError(TCollection _, string argumentName = null) : base($"Collection {argumentName} of type {typeof(TCollection).FullName} is null or empty") { }
    }
}
