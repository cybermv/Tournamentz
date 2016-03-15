namespace Tournamentz.TestConsole
{
    using Autofac;
    using BL.CommandHandlers;
    using BL.Commands;
    using BL.Core;
    using BL.Core.Command;
    using BL.Validators;
    using DAL;
    using DAL.Core;
    using DAL.Entity;
    using System;
    using System.Data.Entity;

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
            builder.RegisterType<PlayerCommandHandler>().AsImplementedInterfaces();
            builder.RegisterType<PlayerValidators.UsernameValidation>().AsImplementedInterfaces();

            IContainer container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(container).As<IContainer>();
            builder.Update(container);

            using (IExecutionContext context = container.Resolve<IExecutionContext>())
            {
                ICommandGate createGate = context.Services
                    .Resolve<ICommandGate<PlayerCommands.Create>>();

                ICommand createCmd = new PlayerCommands.Create
                {
                    ExecutionContext = context,
                    Nickname = "vele",
                    Name = "Mateo",
                    Surname = "Velenik"
                };

                ICommandResult result = createGate.Run(createCmd);

                Player rv = context.UnitOfWork.Repository<Player>().FindById((Guid)result.ReturnValue);

                context.UnitOfWork.Rollback();
            }

            Console.ReadKey();
        }
    }
}