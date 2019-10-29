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
            var file = new FileInfo(filePath);
            using (var originalFileStream = file.OpenRead())
            {
                if (!IsFileValid(file))
                {
                    return;
                }

                output ??= file.DirectoryName;

                var compressedFileName = Path.GetFullPath(Path.Combine(output, file.Name + ".cmp"));
                using (var compressedFileStream = File.Create(compressedFileName))
                {
                    using (var compressionStream = new DeflateStream(compressedFileStream, CompressionLevel.Optimal))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        public void CompressDirectory(string directoryPath, string output)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            directorySelected.GetFiles().ToList().ForEach(file => CompressFile(file.FullName, output));
        }

        private static bool IsFileValid(FileInfo file)
        {
            var isFileHidden = (File.GetAttributes(file.FullName) & FileAttributes.Hidden) == FileAttributes.Hidden;
            return !isFileHidden && file.Extension != ".cmp";
        }
    }
}
