using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Messerli.Utility.Extension;
using Microsoft.VisualBasic;

namespace DictionaryCompressor
{
    internal class FileEnumerator : IFileEnumerator
    {
        public IReadOnlyCollection<FileInfo> GetFiles(DirectoryInfo path)
        {
            var files = new List<FileInfo>();
            path.GetFiles().ForEach(file => files.Add(file));
            return files;
        }
    }
}
