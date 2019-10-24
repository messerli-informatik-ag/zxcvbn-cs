using Autofac;

namespace DictionaryCompressor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var container = CreateCompositionRoot())
            {
                container.Resolve<IApplication>().Run(args);
            }
        }

        private static IContainer CreateCompositionRoot()
        {
            return new CompositionRootBuilder()
                .RegisterModule(new DictionaryCompressorModule())
                .Build();
        }
    }
}
