using System;

namespace OperationResults.OperationErrors.Io
{
    public abstract class IoError : OperationError
    {
        public string Path { get; set; }

        protected IoError(Exception ex, string path) : base(ex, path)
            => Path = path;

        protected IoError(string path) : base(path)
            => Path = path;

        public override string Message { get => GetMessage(base.Message); protected set => base.Message = value; }

        protected abstract string GetMessage(string path);
    }

    public sealed class UnhandledIoError : IoError
    {
        public UnhandledIoError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Unhandled IO exception by path {path}";
    }
}
