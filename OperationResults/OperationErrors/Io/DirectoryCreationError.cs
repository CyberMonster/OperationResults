using System;

namespace OperationResults.OperationErrors.Io
{
    public class DirectoryCreationError : IoError
    {
        public DirectoryCreationError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Error on directory creration by path {path}";
    }
}
