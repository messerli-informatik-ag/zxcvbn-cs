using DictionaryCompressor.CommandLineParsing;

namespace DictionaryCompressor
{
    internal class Application : IApplication
    {
        private readonly ICommandLineParser _commandLineParser;

        public Application(ICommandLineParser commandLineParser)
        {
            _commandLineParser = commandLineParser;
        }

        public void Run(string[] args)
        {
            _commandLineParser.Parse(args);
        }
    }
}
