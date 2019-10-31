using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Messerli.FileOpeningBuilder;
using Messerli.Utility.Extension;

namespace DictionaryCompressor
{
    internal class FileCompressor : IFileCompressor
    {
        private readonly IFileOpeningBuilder _fileOpeningBuilder;
        private readonly IFileCompression _fileCompression;
        private readonly IFileEnumerator _fileEnumerator;

        public FileCompressor(IFileOpeningBuilder fileOpeningBuilder, IFileCompression fileCompression, IFileEnumerator fileEnumerator)
        {
            _fileOpeningBuilder = fileOpeningBuilder;
            _fileCompression = fileCompression;
            _fileEnumerator = fileEnumerator;
        }

        public void CompressFile(string filePath, string output)
        {
            using (var uncompressedStream = _fileOpeningBuilder
                .Read(true)
                .Open(filePath))

            using (var compressedStream = _fileOpeningBuilder
                .Create(true)
                .Write(true)
                .Open(CompressedFilePath(filePath, output)))
            {
                _fileCompression.Compress(uncompressedStream, compressedStream);
            }
        }

        public void CompressDirectory(string directoryPath, string output)
        {
            GetValidUncompressedDirectoryFiles(directoryPath).ForEach(file => CompressFile(file.FullName, output));
        }

        private IReadOnlyCollection<FileInfo> GetValidUncompressedDirectoryFiles(string directoryPath)
        {
            return _fileEnumerator.GetFiles(new DirectoryInfo(directoryPath))
                .Where(IsFileValid).ToList();
        }

        private string CompressedFilePath(string filePath, string output)
        {
            var file = new FileInfo(filePath);
            output ??= file.DirectoryName;
            return Path.GetFullPath(Path.Combine(output, file.Name + _fileCompression.FileExtension));
        }

        private bool IsFileValid(FileInfo file)
        {
            var isFileHidden = (File.GetAttributes(file.FullName) & FileAttributes.Hidden) == FileAttributes.Hidden;
            return !isFileHidden && file.Extension != _fileCompression.FileExtension;
        }
    }
}
