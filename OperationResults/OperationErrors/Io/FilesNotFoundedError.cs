﻿namespace OperationResults.OperationErrors.Io
{
    public class FilesNotFoundedError : IoError
    {
        public string Pattern { get; set; }

        private FilesNotFoundedError(string path) : base(path) { }
        public FilesNotFoundedError(string path, string pattern) : base(path)
            => Pattern = pattern;

        protected override string GetMessage(string path)
            => $"Files by path {path} is not found";
    }
}
