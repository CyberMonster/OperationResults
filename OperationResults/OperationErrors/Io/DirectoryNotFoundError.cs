namespace OperationResults.OperationErrors.Io
{
    public class DirectoryNotFoundError : IoError
    {
        public DirectoryNotFoundError(string path = null) : base(path)
            => Path = path;

        protected override string GetMessage(string path)
            => $"Directory {path} is not found";
    }
}
