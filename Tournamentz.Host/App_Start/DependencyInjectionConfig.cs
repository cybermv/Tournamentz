namespace Tournamentz.Host
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using BL.CommandHandlers;
    using BL.Commands;
    using BL.Core;
    using BL.Core.Command;
    using BL.Core.Command.Interface;
    using BL.Core.Logging;
    using BL.Core.Query;
    using BL.Core.Query.Interface;
    using BL.Queries;
    using BL.Validators;
    using DAL;
    using DAL.Core;
    using System.Data.Entity;

    public static class DependencyInjectionConfig
    {
        public static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            RegisterMvcComponents(builder);
            Register(builder);

            IContainer container = builder.Build();

            ContainerBuilder secondary = new ContainerBuilder();
            secondary.RegisterInstance(container).As<IContainer>();
            secondary.Update(container);

            return container;
        }

        private static void RegisterMvcComponents(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(DependencyInjectionConfig).Assembly);
            //.PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);

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

            // register BL components
            // TODO: move to Tournamentz.BL

            builder.RegisterType<BasicCommandGate<PlayerCommands.Create>>().As<ICommandGate<PlayerCommands.Create>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.CreateOrRetrieve>>().As<ICommandGate<PlayerCommands.CreateOrRetrieve>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.Update>>().As<ICommandGate<PlayerCommands.Update>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.Delete>>().As<ICommandGate<PlayerCommands.Delete>>();
            builder.RegisterType<PlayerCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.UniqueUsernameValidation>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.CannotDeleteUsedPlayer>().AsImplementedInterfaces();

            builder.RegisterType<BasicQueryGate<PlayerQueries.All>>().As<IQueryGate<PlayerQueries.All>>();
            builder.RegisterType<BasicQueryGate<PlayerQueries.Dropdown>>().As<IQueryGate<PlayerQueries.Dropdown>>();
            builder.RegisterType<PlayerQueries.All>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.Dropdown>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.FilteredByName>().AsImplementedInterfaces();
        }
    }
}