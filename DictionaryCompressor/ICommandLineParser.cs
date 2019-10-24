using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DictionaryCompressor
{
    internal interface ICommandLineParser
    {
        ICollection<FileSystemInfo> Parse(string[] args);
    }
}