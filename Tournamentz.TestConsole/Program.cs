namespace Tournamentz.TestConsole
{
    using Autofac;
    using BL.CommandHandlers;
    using BL.Commands;
    using BL.Core;
    using BL.Core.Command;
    using BL.Core.Query;
    using BL.Queries;
    using BL.Validators;
    using DAL;
    using DAL.Core;
    using DAL.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            // TODO: move dependencies in main host project
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BasicExecutionContext>().As<IExecutionContext>();
            builder.RegisterType<BasicUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<TournamentzModelContext>().As<DbContext>();

            builder.RegisterType<BasicCommandGate<PlayerCommands.Create>>().As<ICommandGate<PlayerCommands.Create>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.Update>>().As<ICommandGate<PlayerCommands.Update>>();
            builder.RegisterType<PlayerCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.UsernameValidation>().AsImplementedInterfaces();

            builder.RegisterType<BasicQueryGate<PlayerQueries.All>>().As<IQueryGate<PlayerQueries.All>>();
            builder.RegisterType<BasicQueryGate<PlayerQueries.Dropdown>>().As<IQueryGate<PlayerQueries.Dropdown>>();
            builder.RegisterType<PlayerQueries.All>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.Dropdown>().AsImplementedInterfaces();

            IContainer container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(container).As<IContainer>();
            builder.Update(container);

            using (IExecutionContext context = container.Resolve<IExecutionContext>())
            {
                IQueryGate<PlayerQueries.All> playersGate = context.Services
                    .Resolve<IQueryGate<PlayerQueries.All>>();

                IQueryResult<PlayerQueries.All> queryResult = playersGate.Query(context);

                List<PlayerQueries.All> all = queryResult.Query.ToList();

                ICommandGate createGate = context.Services
                    .Resolve<ICommandGate<PlayerCommands.Create>>();

                ICommand createCmd = new PlayerCommands.Create
                {
                    ExecutionContext = context,
                    Nickname = "vele",
                    Name = "Mateo",
                    Surname = "Veleniććć"
                };

                ICommandResult result = createGate.Run(createCmd);

                ICommandGate updateGate = context.Services
                    .Resolve<ICommandGate<PlayerCommands.Update>>();

                PlayerCommands.Update update = new PlayerCommands.Update
                {
                    ExecutionContext = context,
                    Id = (Guid)result.ReturnValue,
                    Name = "Mateo",
                    Surname = "Velenik"
                };

                ICommandResult updateResult = updateGate.Run(update);

                Console.WriteLine(updateResult.ToString());

                Player rv = context.UnitOfWork.Repository<Player>().FindById((Guid)result.ReturnValue);

                context.UnitOfWork.Rollback();
            }

            Console.ReadKey();
        }
    }
}