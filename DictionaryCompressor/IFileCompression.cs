using System.IO;

namespace DictionaryCompressor
{
    internal interface IFileCompression
    {
        string FileExtension { get; }

        void Compress(Stream from, Stream to);
    }
}
