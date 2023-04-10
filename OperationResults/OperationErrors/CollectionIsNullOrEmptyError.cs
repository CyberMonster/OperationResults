using System.Collections;

namespace OperationResults.OperationErrors
{
    public class CollectionIsNullOrEmptyError : OperationError
    {
        private CollectionIsNullOrEmptyError(string message = null) : base(message) { }
        public CollectionIsNullOrEmptyError(IEnumerable collection) : base($"Collection is null or empty. Collection type: {collection.GetType().FullName}") { }
    }
}
