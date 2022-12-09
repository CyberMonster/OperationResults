namespace OperationResults.OperationErrors.Io
{
    public class FileNotFoundError : IoError
    {
        public FileNotFoundError(string path) : base(path) { }

        protected override string GetMessage(string path)
            => $"File {path} is not found";
    }
}
