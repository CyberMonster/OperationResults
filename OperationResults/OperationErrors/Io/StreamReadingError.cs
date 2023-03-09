using System;

namespace OperationResults.OperationErrors.Io
{
    public class StreamReadingError : IoError
    {
        public StreamReadingError(string path = null) : base(path) { }

        public StreamReadingError(Exception ex, string path = null) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Can't read stream";
    }
}
