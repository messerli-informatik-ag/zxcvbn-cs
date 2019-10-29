using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DictionaryCompressor
{
    public interface IFileEnumerator
    {
        IReadOnlyCollection<FileInfo> GetFiles(DirectoryInfo path);
    }
}
