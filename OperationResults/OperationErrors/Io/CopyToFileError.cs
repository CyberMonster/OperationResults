﻿using System;

namespace OperationResults.OperationErrors.Io
{
    public class CopyToFileError : IoError
    {
        private CopyToFileError(string path) : base(path) { }
        public CopyToFileError(Exception ex, string path) : base(ex, path) { }

        protected override string GetMessage(string path)
            => $"Can't create or copy file by path {path}";
    }
}
