using Autofac;

namespace DictionaryCompressor
{
    internal class Program
    {
        private static IContainer CreateCompositionRoot()
        {
            return new CompositionRootBuilder()
                .RegisterModule(new DictionaryCompressorModule())
                .Build();
        }

        public static void Main(string[] args)
        {
            using (var container = CreateCompositionRoot())
            {
                container.Resolve<IApplication>().Run(args);
            }
        }
    }
}
