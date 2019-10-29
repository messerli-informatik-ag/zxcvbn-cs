using Autofac;
using Autofac.Core;

namespace DictionaryCompressor
{
    internal class CompositionRootBuilder
    {
        private readonly ContainerBuilder _builder = new ContainerBuilder();

        public CompositionRootBuilder RegisterModule<T>()
            where T : IModule, new()
        {
            _builder.RegisterModule<T>();
            return this;
        }

        public CompositionRootBuilder RegisterModule(IModule module)
        {
            _builder.RegisterModule(module);
            return this;
        }

        public IContainer Build()
        {
            return _builder.Build();
        }
    }
}
