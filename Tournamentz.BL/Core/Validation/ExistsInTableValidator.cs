namespace Tournamentz.BL.Core.Validation
{
    using Attribute;
    using Rule;
    using System;
    using System.Linq;
    using System.Reflection;
    using Command.Interface;
    using DAL.Core;

    public static class ExistsInTableValidator
    {
        public static BusinessRuleCollection ValidateAttributes<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            BusinessRuleCollection rules = new BusinessRuleCollection();

            var propertiesToValidate = typeof(TCommand)
                .GetProperties()
                .Select(p => new
                {
                    Property = p,
                    Attribute = p.GetCustomAttribute<ExistsInTableAttribute>()
                })
                .Where(p => p.Attribute != null)
                .ToList();

            Type uowType = command.ExecutionContext.UnitOfWork.GetType();
            MethodInfo genericRepoMethod = uowType.GetMethod("Repository");

            foreach (var propToValidate in propertiesToValidate)
            {
                if (propToValidate.Property.PropertyType != typeof(Guid))
                {
                    throw new InvalidOperationException(
                        string.Format("Property '{0}' on ICommand '{1}' is not a Guid",
                            propToValidate.Property.Name,
                            propToValidate.Property.DeclaringType.Name));
                }

                MethodInfo repoMethod = genericRepoMethod.MakeGenericMethod(propToValidate.Attribute.EntityType);
                IRepository repoInstance = (IRepository) repoMethod.Invoke(command.ExecutionContext.UnitOfWork, null);

                Guid value = (Guid)propToValidate.Property.GetValue(command);
                object foundEntity = repoInstance.FindById(value);

                // TODO: localize
                rules.Add(new BusinessRule(
                    foundEntity != null,
                    string.Format("Polje {0} je obavezno", propToValidate.Property.Name),
                    propToValidate.Property.Name));
            }

            return rules;
        }
    }
}