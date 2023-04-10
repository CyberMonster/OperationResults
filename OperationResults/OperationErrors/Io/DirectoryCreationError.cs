using System;

namespace OperationResults.OperationErrors.Io
{
    public class DirectoryCreationError : IoError
    {
        private DirectoryCreationError(string path) : base(path) { }
        public DirectoryCreationError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Error on directory creation by path {path}";
    }
}
