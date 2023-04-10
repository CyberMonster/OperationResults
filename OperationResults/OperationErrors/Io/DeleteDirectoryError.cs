using System;

namespace OperationResults.OperationErrors.Io
{
    public class DeleteDirectoryError : IoError
    {
        private DeleteDirectoryError(string path) : base(path) { }
        public DeleteDirectoryError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Can't delete directory by path {path}";
    }
}
