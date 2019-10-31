using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using DictionaryCompressor;
using Messerli.FileOpeningBuilder;
using Messerli.Test.Utility;
using Moq;
using Xunit;

namespace DictionaryCompressorTest
{
    public class IntegrationTest
    {
        private readonly FileEnumerator _fileEnumerator;
        private readonly FileCompression _fileCompression;
        private readonly FileOpeningBuilder _fileOpeningBuilder;
        private readonly FileCompressor _fileCompressor;
        private readonly TestFile _testFile;

        public IntegrationTest()
        {
            _fileOpeningBuilder = new FileOpeningBuilder();
            _fileEnumerator = new FileEnumerator();
            _fileCompression = new FileCompression();

            _fileCompressor = new FileCompressor(_fileOpeningBuilder, _fileCompression, _fileEnumerator);

            _testFile = TestFile.Create("file1.lst");
        }

        [Fact]
        public void CompressFileTest()
        {
            Compress(
                (testEnvironmentProvider) =>
                {
                    var filePath = Path.Combine(Path.GetTempPath(), testEnvironmentProvider.RootDirectory, _testFile.RelativeFilePath);

                    _fileCompressor.CompressFile(
                        filePath,
                        testEnvironmentProvider.RootDirectory);
                });
        }

        [Fact]
        public void CompressDirectoryTest()
        {
            Compress(
                (testEnvironmentProvider) =>
                {
                    _fileCompressor.CompressDirectory(
                        testEnvironmentProvider.RootDirectory,
                        testEnvironmentProvider.RootDirectory);
                });
        }

        private void Compress(Action<TestEnvironmentProvider> func)
        {
            using (var testEnvironmentBuilder = new TestEnvironmentBuilder()
                .AddTestFile(_testFile)
                .Build())
            {
                var filePath = Path.Combine(Path.GetTempPath(), testEnvironmentBuilder.RootDirectory, _testFile.RelativeFilePath);

                var fileCompressor = new FileCompressor(_fileOpeningBuilder, _fileCompression, _fileEnumerator);
                func(testEnvironmentBuilder);

                var compressedFilePath = Path.Combine(filePath + _fileCompression.FileExtension);
                Assert.True(File.Exists(compressedFilePath));
            }
        }
    }
}
