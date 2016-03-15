namespace Tournamentz.BL.Core.Command
{
    using Autofac;
    using Rule;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Validation;

    public class BasicCommandGate<TCommand> : ICommandGate<TCommand>
        where TCommand : ICommand
    {
        public ICommandResult Run(TCommand command)
        {
            CommandResult result = new CommandResult();

            // 1. validate broken rules
            IEnumerable<IValidator<TCommand>> validators = command.ExecutionContext.Services
                .Resolve<IEnumerable<IValidator<TCommand>>>();

            foreach (IValidator<TCommand> validator in validators)
            {
                BusinessRuleCollection validated = validator.Validate(command);
                result.BusinessRules.Add(validated);
            }

            if (result.Status != CommandResultStatus.Success)
            {
                // TODO: log broken rules
                return result;
            }

            // 2. execute handler
            ICommandHandler<TCommand> handler = command.ExecutionContext.Services
                .Resolve<ICommandHandler<TCommand>>();

            try
            {
                handler.Handle(command);
            }
            catch (Exception ex)
            {
                // TODO: log exception
                result.Exception = ex;
                return result;
            }

            result.BusinessRules.Add(handler.Result.BusinessRules);
            result.Exception = handler.Result.Exception;
            result.ReturnValue = handler.Result.ReturnValue;

            // TODO: log success/broken rules
            return result;
        }

        public ICommandResult Run(ICommand command)
        {
            if (typeof(TCommand) != command.GetType())
            {
                throw new InvalidOperationException(
                    string.Format("The command gate '{0}' cannot run command '{1}'",
                        this.GetType().Name,
                        command.GetType().Name));
            }

            MethodInfo runMethod = this.GetType()
                .GetMethods()
                .Single(m => m.Name == "Run" &&
                             m.GetParameters().Length == 1 &&
                             m.GetParameters()[0].ParameterType == command.GetType());

            object returnObj = runMethod.Invoke(this, new object[] { command });
            return (ICommandResult)returnObj;
        }
    }
}