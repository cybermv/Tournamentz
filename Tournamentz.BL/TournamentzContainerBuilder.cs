namespace Tournamentz.BL
{
    using System.Data.Entity;
    using Autofac;
    using CommandHandlers;
    using Commands;
    using Core;
    using Core.Command;
    using Core.Command.Interface;
    using Core.Logging;
    using Core.Query;
    using Core.Query.Interface;
    using DAL;
    using DAL.Core;
    using Queries;
    using Validators;

    public class TournamentzContainerBuilder
    {
        public IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BasicExecutionContext>().As<IExecutionContext>();
            builder.RegisterType<BasicUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<TournamentzModelContext>().As<DbContext>();
            builder.RegisterType<NLogWrappedLogger>().As<ILogger>();

            builder.RegisterType<BasicCommandGate<PlayerCommands.Create>>().As<ICommandGate<PlayerCommands.Create>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.Update>>().As<ICommandGate<PlayerCommands.Update>>();
            builder.RegisterType<PlayerCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.UniqueUsernameValidation>().AsImplementedInterfaces();

            builder.RegisterType<BasicQueryGate<PlayerQueries.All>>().As<IQueryGate<PlayerQueries.All>>();
            builder.RegisterType<BasicQueryGate<PlayerQueries.Dropdown>>().As<IQueryGate<PlayerQueries.Dropdown>>();
            builder.RegisterType<PlayerQueries.All>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.Dropdown>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.FilteredByName>().AsImplementedInterfaces();

            IContainer container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(container).As<IContainer>();
            builder.Update(container);

            return container;
        }
    }
}
