using System;

namespace OperationResults.OperationErrors.Io
{
    public class CanNotOpenFileError : IoError
    {
        public CanNotOpenFileError(Exception ex, string path) : base(ex, path)
            => Path = path;

        protected override string GetMessage(string path)
            => $"Can't open file {path} for reading";
    }
}
