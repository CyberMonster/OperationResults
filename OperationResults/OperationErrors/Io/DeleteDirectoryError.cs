using System;

namespace OperationResults.OperationErrors.Io
{
    public class DeleteDirectoryError : IoError
    {
        public DeleteDirectoryError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Can't delete directory by path {path}";
    }
}
