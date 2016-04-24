namespace Tournamentz.Host
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using BL.Core;
    using BL.Core.Logging;
    using DAL;
    using DAL.Core;
    using System.Data.Entity;
    using BL;

    public static class DependencyInjectionConfig
    {
        public static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            RegisterMvcComponents(builder);
            TournamentzContainerBuilder.RegisterTournamentzModule(builder);
            Register(builder);
            
            IContainer container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(container).As<IContainer>();
            builder.Update(container);

            return container;
        }

        private static void RegisterMvcComponents(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(DependencyInjectionConfig).Assembly);

            builder.RegisterModelBinders(typeof(DependencyInjectionConfig).Assembly);
            builder.RegisterModelBinderProvider();

            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            builder.RegisterApiControllers(typeof(DependencyInjectionConfig).Assembly);
        }

        private static void Register(ContainerBuilder builder)
        {
            // the model context can be resolved multiple times and returns
            // a new instance each time
            builder.RegisterType<TournamentzModelContext>()
                .As<DbContext>()
                .As<TournamentzModelContext>()
                .InstancePerDependency()
                .ExternallyOwned();

            // each UnitOfWork gets it's own model context
            builder.RegisterType<BasicUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerDependency()
                .ExternallyOwned();

            // there is only one ExecutionContext per request, with it's own
            // UnitOfWork and model context
            builder.RegisterType<BasicExecutionContext>()
                .As<IExecutionContext>()
                .InstancePerRequest()
                .ExternallyOwned();

            // the Logger is a singleton
            builder.RegisterType<NLogWrappedLogger>()
                .As<ILogger>()
                .SingleInstance();
        }
    }
}