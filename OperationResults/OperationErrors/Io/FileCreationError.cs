using System;

namespace OperationResults.OperationErrors.Io
{
    public class FileCreationError : IoError
    {
        private FileCreationError(string path) : base(path) { }
        public FileCreationError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Can't create file {path}";
    }
}
