namespace Tournamentz.BL.Core.Validation
{
    using Attribute;
    using DAL.Entity;
    using Rule;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class RoleValidator
    {
        public static BusinessRuleCollection ValidateAttributes<T>(IExecutionContext context)
        {
            BusinessRuleCollection rules = new BusinessRuleCollection();

            List<RequiresRoleAttribute> requiresRoleAttributes = typeof(T)
                .GetCustomAttributes<RequiresRoleAttribute>()
                .ToList();

            foreach (RequiresRoleAttribute attribute in requiresRoleAttributes)
            {
                ApplicationUserRole role = context.User.Roles
                    .FirstOrDefault(r => r.RoleId == attribute.RoleId);

                // TODO: localize
                rules.Add(new BusinessRule(
                    role != null,
                    "Za akciju je potrebna rola {0}"));
            }

            return rules;
        }
    }
}