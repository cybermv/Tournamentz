namespace Tournamentz.DAL.Identity
{
    using Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;

    public class ApplicationRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    {
        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}