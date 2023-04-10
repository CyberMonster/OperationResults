using System;

namespace OperationResults.OperationErrors.Io
{
    public class WrongPatternError : IoError
    {
        public string Pattern { get; set; }

        private WrongPatternError(string path) : base(path) { }
        public WrongPatternError(Exception ex, string path, string pattern) : base(ex, path)
            => Pattern = pattern;
        protected override string GetMessage(string path)
            => $"Can't use pattern {Pattern} for path {path}";
    }
}
