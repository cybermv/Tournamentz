namespace Tournamentz.BL.Core.Command
{
    using Autofac;
    using Logging;
    using Rule;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Interface;
    using Validation;

    public class BasicCommandGate<TCommand> : ICommandGate<TCommand>
        where TCommand : ICommand
    {
        public ICommandResult Run(TCommand command)
        {
            ILogger logger = command.ExecutionContext.Services.Resolve<ILogger>();
            CommandResult result = new CommandResult();

            // 1. validate RequiresRole attributes
            BusinessRuleCollection permissionRules = RoleValidator.ValidateAttributes<TCommand>(command.ExecutionContext);
            result.PermissionRules.Add(permissionRules);

            if (result.Status != CommandResultStatus.Success)
            {
                logger.LogCommand<TCommand>(command, result);
                return result;
            }

            // 2. validate ExistsInTable attributes
            BusinessRuleCollection rules = ExistsInTableValidator.ValidateAttributes(command);
            result.BusinessRules.Add(rules);

            if (result.Status != CommandResultStatus.Success)
            {
                logger.LogCommand<TCommand>(command, result);
                return result;
            }

            // 3. execute all validators
            IEnumerable<IValidator<TCommand>> validators = command.ExecutionContext.Services
                .Resolve<IEnumerable<IValidator<TCommand>>>();

            foreach (IValidator<TCommand> validator in validators)
            {
                BusinessRuleCollection validated = validator.Validate(command);
                result.BusinessRules.Add(validated);
            }

            if (result.Status != CommandResultStatus.Success)
            {
                logger.LogCommand<TCommand>(command, result);
                return result;
            }

            // 4. execute handler
            ICommandHandler<TCommand> handler = command.ExecutionContext.Services
                .Resolve<ICommandHandler<TCommand>>();

            try
            {
                handler.Handle(command);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                logger.LogCommand<TCommand>(command, result);
                return result;
            }

            result.BusinessRules.Add(handler.Result.BusinessRules);
            result.PermissionRules.Add(handler.Result.PermissionRules);
            result.Exception = handler.Result.Exception;
            result.ReturnValue = handler.Result.ReturnValue;

            logger.LogCommand<TCommand>(command, result);
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