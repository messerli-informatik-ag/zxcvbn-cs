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
        public IReadOnlyCollection<string> GetFiles(string path)
        {
            var files = new List<string>();
            Directory.GetFiles(path).ForEach(file => files.Add(file));
            return files;
        }
    }
}
