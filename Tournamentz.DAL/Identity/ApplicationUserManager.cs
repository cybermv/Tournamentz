namespace Tournamentz.DAL.Identity
{
    using Entity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;

    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(DbContext context)
            : base(new ApplicationUserStore(context))
        {
        }
    }
}