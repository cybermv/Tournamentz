namespace Tournamentz.BL.Core.Attribute
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class RequiresRoleAttribute : Attribute
    {
        public RequiresRoleAttribute(string roleGuid)
        {
            try
            {
                this.RoleId = new Guid(roleGuid);
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    string.Format("Wrong argument to the RequiresRole attribute; value '{0}' is not a Guid",
                        roleGuid ?? "null"));
            }
        }

        public Guid RoleId { get; private set; }
    }
}