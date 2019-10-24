using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;

namespace DictionaryCompressor.CommandLineParsing.Parser
{
    public sealed class CommandLineParser : ICommandLineParser
    {
        private readonly IFileCompressor _fileCompressor;
        private Command _rootCommand;

        public CommandLineParser(IFileCompressor fileCompressor)
        {
            _fileCompressor = fileCompressor;
        }

        public void Parse(string[] args)
        {
            _rootCommand = new RootCommand
            {
                new Option(
                    "--files",
                    "Files location")
                {
                    Argument = new Argument<IEnumerable<string>>(),
                },
                new Option(
                    "--directories",
                    "Directories Location")
                {
                    Argument = new Argument<IEnumerable<string>>(),
                },
                new Option(
                    "--output",
                    "Output path")
                {
                    Argument = new Argument<string>(),
                },
            };
            IEnumerable<FileSystemInfo> paths = new List<FileSystemInfo>();
            _rootCommand.Handler = CommandHandler.Create<List<string>, List<string>, string>((files, directories, output) =>
                {
                    files?.ForEach(file => _fileCompressor.CompressFile(file, output));
                    directories?.ForEach(directory => _fileCompressor.CompressDirectory(directory, output));
                });
            _rootCommand.InvokeAsync(args);
        }
    }
}