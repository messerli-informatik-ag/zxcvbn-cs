using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DictionaryCompressor
{
    internal class FileCompression : IFileCompression
    {
        string IFileCompression.FileExtension => ".cmd";

        public void Compress(Stream from, Stream to)
        {
            using (var compressionStream = new DeflateStream(to, CompressionLevel.Optimal))
            {
                from.CopyTo(compressionStream);
            }
        }
    }
}
