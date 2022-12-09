using System;

using IOPath = System.IO.Path;

namespace OperationResults.OperationErrors.Io
{
    public class WrongPathError : IoError
    {
        public char[] InvalidFileNameChars => IOPath.GetInvalidFileNameChars();
        public char[] InvalidPathChars => IOPath.GetInvalidPathChars();

        public string FileName => IOPath.GetFileName(Path);
        public string DirectoryPath => IOPath.GetDirectoryName(Path);

        public bool IsFileNameInvalid => FileNameWrongPosition >= 0;
        public bool IsPathInvalid => PathWrongPosition >= 1;

        public int FileNameWrongPosition => FileName.IndexOfAny(InvalidFileNameChars);
        public int PathWrongPosition => DirectoryPath.IndexOfAny(InvalidPathChars);

        public WrongPathError(Exception ex, string path) : base(ex, path) { }
        public WrongPathError(string path) : base(path) { }

        protected override string GetMessage(string path)
            => $"Error on getting full path of {path}";
    }
}
