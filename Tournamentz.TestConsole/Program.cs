namespace Tournamentz.TestConsole
{
    using Autofac;
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
    using DAL.Entity;
    using DAL.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using BL;

    internal class Program
    {
        private static void Main()
        {
            IContainer container = new TournamentzContainerBuilder().Build();

            using (IExecutionContext context = container.Resolve<IExecutionContext>())
            {
                ApplicationUserManager manager = new ApplicationUserManager(context.UnitOfWork);
                context.User = manager.Find("prvi.user", "prvi.user");

                IParameteredQueryGate<PlayerQueries.FilteredByName, string> parameteredQueryGate =
                    container.Resolve<IParameteredQueryGate<PlayerQueries.FilteredByName, string>>();

                IQueryResult<PlayerQueries.FilteredByName> result1 = parameteredQueryGate.Query(context, "ti");

                List<PlayerQueries.FilteredByName> list1 = result1.Query.ToList();

                

                IQueryGate<PlayerQueries.All> playersGate = context.Services
                    .Resolve<IQueryGate<PlayerQueries.All>>();

                IQueryResult<PlayerQueries.All> queryResult1 = playersGate.Query(context);

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