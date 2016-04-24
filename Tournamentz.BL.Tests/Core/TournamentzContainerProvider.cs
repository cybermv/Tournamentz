namespace Tournamentz.BL.Tests.Core
{
    using Autofac;

    public static class TournamentzContainerProvider
    {
        private static IContainer _container;
        private static readonly object Lock = new object();

        public static IContainer Instance
        {
            get
            {
                if (_container == null)
                {
                    lock (Lock)
                    {
                        if (_container == null)
                        {
                            ContainerBuilder builder = new ContainerBuilder();
                            TournamentzContainerBuilder.RegisterTournamentzModule(builder);
                            IContainer container = builder.Build();

                            builder = new ContainerBuilder();
                            builder.RegisterInstance(container).As<IContainer>();
                            builder.Update(container);

                            _container = container;
                        }
                    }
                }

                return _container;
            }
        }
    }
}
