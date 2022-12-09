using System;

namespace OperationResults.OperationErrors
{
    public class WrongXmlError : OperationError
    {
        public WrongXmlError(string message) : base(message) { }

        public WrongXmlError(Exception ex, string message) : base(ex, message) { }
    }
}
