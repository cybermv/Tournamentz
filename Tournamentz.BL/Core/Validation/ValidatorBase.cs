namespace Tournamentz.BL.Core.Validation
{
    using Command;
    using Rule;
    using System;
    using System.Linq;
    using System.Reflection;

    public class ValidatorBase : IValidator
    {
        public virtual BusinessRuleCollection Validate(ICommand command)
        {
            MethodInfo handlingMethod = this.GetType()
                .GetMethods()
                .SingleOrDefault(m => m.Name == "Validate" &&
                                      m.GetParameters().Length == 1 &&
                                      m.GetParameters()[0].ParameterType == command.GetType());

            if (handlingMethod == null)
            {
                throw new MissingMethodException(
                    string.Format("The Validate method for command '{0}' is not present on the '{1}' validator.",
                        command.GetType().FullName,
                        this.GetType().FullName));
            }

            object returnObj = handlingMethod.Invoke(this, new object[] { command });

            return (BusinessRuleCollection)returnObj;
        }
    }
}