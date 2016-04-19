namespace Tournamentz.BL
{
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Core;
    using Core.Command;
    using Core.Command.Interface;
    using Core.Logging;
    using Core.Query;
    using Core.Query.Interface;
    using Core.Validation;
    using DAL;
    using DAL.Core;

    public class TournamentzContainerBuilder
    {
        public IContainer Build()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Assembly currentAssembly = this.GetType().Assembly;

            builder.RegisterType<BasicExecutionContext>().As<IExecutionContext>();
            builder.RegisterType<BasicUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<TournamentzModelContext>().As<DbContext>();
            builder.RegisterType<NLogWrappedLogger>().As<ILogger>();

            builder.RegisterGeneric(typeof (BasicCommandGate<>)).As(typeof (ICommandGate<>));
            builder.RegisterGeneric(typeof (BasicQueryGate<>)).As(typeof (IQueryGate<>));
            builder.RegisterGeneric(typeof (ParameteredQueryGate<,>)).As(typeof (IParameteredQueryGate<,>));

            // register all command handlers
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof (ICommandHandler<>));

            // register all validators
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof (IValidator<>));

            // register all basic query handlers
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof (IQueryHandler<>));
            
            // register all parametered query handlers
            builder.RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof (IParameteredQueryHandler<,>));

            IContainer container = builder.Build();

            builder = new ContainerBuilder();
            builder.RegisterInstance(container).As<IContainer>();
            builder.Update(container);

            return container;
        }
    }
}
