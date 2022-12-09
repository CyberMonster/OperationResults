using System;

namespace OperationResults.OperationErrors.Io
{
    public class DeleteFileError : IoError
    {
        public DeleteFileError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Can't delete file by path {path}";
    }
}
