namespace Tournamentz.BL.Core.Command
{
    using System;
    using System.Linq;
    using System.Reflection;

    public abstract class CommandHandlerBase : ICommandHandler
    {
        public object ReturnValue { get; set; }

        public virtual void Handle(ICommand command)
        {
            MethodInfo handlingMethod = this.GetType()
                .GetMethods()
                .SingleOrDefault(m => m.Name == "Handle" &&
                                      m.GetParameters().Length == 1 &&
                                      m.GetParameters()[0].ParameterType == command.GetType());

            if (handlingMethod == null)
            {
                throw new MissingMethodException(
                    string.Format("The Handle method for command '{0}' is not present on the '{1}' command handler.",
                        command.GetType().Name,
                        this.GetType().Name));
            }

            handlingMethod.Invoke(this, new object[] { command });
        }
    }
}