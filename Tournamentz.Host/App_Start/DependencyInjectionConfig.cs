namespace Tournamentz.Host
{
    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;
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
    using DAL.Identity;
    using Identity;
    using Microsoft.Owin.Security;
    using System.Data.Entity;
    using System.Web;

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
                .As<TournamentzModelContext>();

            // each UnitOfWork gets it's own model context
            builder.RegisterType<BasicUnitOfWork>()
                .As<IUnitOfWork>();

            // there is only one ExecutionContext per request, with it's own
            // UnitOfWork and model context
            builder.RegisterType<BasicExecutionContext>()
                .As<IExecutionContext>()
                .InstancePerRequest();

            // the Logger is a singleton
            builder.RegisterType<NLogWrappedLogger>()
                .As<ILogger>()
                .SingleInstance();

            // there is only one user store per request
            builder.RegisterType<ApplicationUserStore>()
                .AsSelf()
                .InstancePerRequest();

            // there is only one user manager per request
            builder.RegisterType<DAL.Identity.ApplicationUserManager>()
                .AsSelf()
                .OnActivated(args => args.Instance.Configure())
                .InstancePerRequest();

            // TODO: figure out
            //builder.RegisterInstance<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication)
            //    .As<IAuthenticationManager>();

            // there is only one sign in manager per request
            builder.RegisterType<ApplicationSignInManager>()
                .AsSelf()
                .InstancePerRequest();

            // register BL components
            // TODO: move to Tournamentz.BL

            builder.RegisterType<BasicCommandGate<PlayerCommands.Create>>().As<ICommandGate<PlayerCommands.Create>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.Update>>().As<ICommandGate<PlayerCommands.Update>>();
            builder.RegisterType<PlayerCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.UsernameValidation>().AsImplementedInterfaces();

            builder.RegisterType<BasicQueryGate<PlayerQueries.All>>().As<IQueryGate<PlayerQueries.All>>();
            builder.RegisterType<BasicQueryGate<PlayerQueries.Dropdown>>().As<IQueryGate<PlayerQueries.Dropdown>>();
            builder.RegisterType<PlayerQueries.All>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.Dropdown>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.FilteredByName>().AsImplementedInterfaces();
        }
    }
}