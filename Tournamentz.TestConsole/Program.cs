namespace Tournamentz.TestConsole
{
    using Autofac;
    using BL.CommandHandlers;
    using BL.Commands;
    using BL.Core;
    using BL.Core.Command;
    using BL.Core.Logging;
    using BL.Core.Query;
    using BL.Core.Query.Interface;
    using BL.Queries;
    using BL.Validators;
    using DAL;
    using DAL.Core;
    using DAL.Entity;
    using DAL.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using BL.Core.Command.Interface;

    internal class Program
    {
        private static void Main()
        {
            // TODO: move dependencies in main host project
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BasicExecutionContext>().As<IExecutionContext>();
            builder.RegisterType<BasicUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<TournamentzModelContext>().As<DbContext>();
            builder.RegisterType<NLogWrappedLogger>().As<ILogger>();

            builder.RegisterType<BasicCommandGate<PlayerCommands.Create>>().As<ICommandGate<PlayerCommands.Create>>();
            builder.RegisterType<BasicCommandGate<PlayerCommands.Update>>().As<ICommandGate<PlayerCommands.Update>>();
            builder.RegisterType<PlayerCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.UsernameValidation>().AsImplementedInterfaces();

            builder.RegisterType<BasicQueryGate<PlayerQueries.All>>().As<IQueryGate<PlayerQueries.All>>();
            builder.RegisterType<BasicQueryGate<PlayerQueries.Dropdown>>().As<IQueryGate<PlayerQueries.Dropdown>>();
            builder.RegisterType<PlayerQueries.All>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.Dropdown>().AsImplementedInterfaces();
            builder.RegisterType<PlayerQueries.FilteredByName>().AsImplementedInterfaces();

            IContainer container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(container).As<IContainer>();
            builder.Update(container);

            using (IExecutionContext context = container.Resolve<IExecutionContext>())
            {
                var gate = new ParameteredQueryGate<PlayerQueries.FilteredByName, PlayerQueries.FilteredByName.NameParam>();

                IQueryResult<PlayerQueries.FilteredByName> result1 = gate.Query(context,
                    new PlayerQueries.FilteredByName.NameParam { Name = "ti" });

                List<PlayerQueries.FilteredByName> res = result1.Query.ToList();

                ApplicationUserManager manager = new ApplicationUserManager(context.UnitOfWork);
                context.User = manager.Find("prvi.user", "prvi.user");

                IQueryGate<PlayerQueries.All> playersGate = context.Services
                    .Resolve<IQueryGate<PlayerQueries.All>>();

                IQueryResult<PlayerQueries.All> queryResult1 = playersGate.Query(context);

                context.User = manager.Find("admin", "adminadmin");

                IQueryResult<PlayerQueries.All> queryResult2 = playersGate.Query(context);

                List<PlayerQueries.All> allPlayers = queryResult2.Query.ToList();

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

                Player rv = context.UnitOfWork.Repository<Player>().FindById((Guid)result.ReturnValue);

                context.UnitOfWork.Rollback();
            }

            Console.WriteLine("Finished.");
            Console.ReadKey();
        }
    }
}