namespace Tournamentz.DAL.Entity
{
    using Core;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<Guid>, IEntity
    {
        public virtual Player Player { get; set; }
    }

    #region ASP.NET Identity utils

    // TODO: consult this
    // http://www.asp.net/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity
    public class ApplicationUserLogin : IdentityUserLogin<Guid> { }

    public class ApplicationUserRole : IdentityUserRole<Guid> { }

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>, IEntity
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid();
        }
    }

    public class ApplicationUserClaim : IdentityUserClaim<Guid> { }

    #endregion ASP.NET Identity utils
}