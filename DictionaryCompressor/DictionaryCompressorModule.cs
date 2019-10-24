using Autofac;
using Autofac.Core;

namespace DictionaryCompressor
{
    internal class DictionaryCompressorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<CommandLineParser>().As<ICommandLineParser>();
            builder.RegisterType<FileCompressor>().As<IFileCompressor>();
        }
    }
}