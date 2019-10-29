using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace DictionaryCompressor
{
    internal class FileCompressor : IFileCompressor
    {
        public void CompressFile(string filePath, string output)
        {
            FileInfo file = new FileInfo(filePath);
            using (FileStream originalFileStream = file.OpenRead())
            {
                if ((File.GetAttributes(file.FullName) & FileAttributes.Hidden)
                    != FileAttributes.Hidden & file.Extension != ".cmp")
                {
                    output ??= file.DirectoryName;

                    var compressedFileName = Path.GetFullPath(Path.Combine(output, file.Name + ".cmp"));
                    using (FileStream compressedFileStream = File.Create(compressedFileName))
                    {
                        using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionLevel.Optimal))
                        {
                            originalFileStream.CopyTo(compressionStream);
                        }
                    }
                }
            }
        }

        public void CompressDirectory(string directoryPath, string output)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            directorySelected.GetFiles().ToList().ForEach(file => CompressFile(file.FullName, output));
        }
    }
}
