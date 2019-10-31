using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DictionaryCompressor
{
    internal class FileCompression : IFileCompression
    {
        public string FileExtension => ".cmp";

        public void Compress(Stream from, Stream to)
        {
            using (var compressionStream = new DeflateStream(to, CompressionLevel.Optimal))
            {
                from.CopyTo(compressionStream);
            }
        }
    }
}
