using Autofac;
using DictionaryCompressor.CommandLineParsing;
using DictionaryCompressor.CommandLineParsing.Parser;
using Messerli.FileOpeningBuilder;

namespace DictionaryCompressor
{
    internal class DictionaryCompressorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<CommandLineParser>().As<ICommandLineParser>();
            builder.RegisterType<FileCompressor>().As<IFileCompressor>();
            builder.RegisterType<FileOpeningBuilder>().As<IFileOpeningBuilder>();
            builder.RegisterType<FileCompression>().As<IFileCompression>();
        }
    }
}
