namespace Tournamentz.BL.Core.Command
{
    using Autofac;
    using Interface;
    using Rule;

    public abstract class CommandHandlerBase
    {
        protected CommandHandlerBase()
        {
            this.Result = new CommandResult();
        }

        public ICommandResult Result { get; private set; }

        public bool CannotContinue { get { return this.Result.Status != CommandResultStatus.Success; } }

        public void AddRule(BusinessRule rule)
        {
            this.Result.BusinessRules.Add(rule);
        }

        public object RunCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            ICommandGate<TCommand> commandGate = command.ExecutionContext.Services
                .Resolve<ICommandGate<TCommand>>();

            ICommandResult result = commandGate.Run(command);

            this.Result.BusinessRules.Add(result.BusinessRules);
            this.Result.PermissionRules.Add(result.PermissionRules);
            if (result.Exception != null)
            {
                this.Result.Exception = result.Exception;
            }

            return result.ReturnValue;
        }

        public TResult RunCommand<TCommand, TResult>(TCommand command)
            where TCommand : ICommand
        {
            return (TResult) this.RunCommand(command);
        }
    }
}