using System;

namespace DictionaryCompressor
{
    internal class Application : IApplication
    {
        private readonly ICommandLineParser _commandLineParser;
        private readonly IFileCompressor _fileCompressor;

        public Application(ICommandLineParser commandLineParser, IFileCompressor fileCompressor)
        {
            _commandLineParser = commandLineParser;
            _fileCompressor = fileCompressor;
        }

        public void Run(string[] args)
        {
            var paths = _commandLineParser.Parse(args);
            foreach(var path in paths)
            {
                _fileCompressor.Compress(path);
            }
        }
    }
}